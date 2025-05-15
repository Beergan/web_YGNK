using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;
using Newtonsoft.Json;
using SLK.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

public static class PropertyInfoExtensions
{
    public static bool IsStatic(this PropertyInfo source, bool nonPublic = false)
        => source.GetAccessors(nonPublic).Any(x => x.IsStatic);
}

public class ComponentSynchronizer
{
    private const string _defautUsing = "using System;using SLK.Common;using System.Collections.Generic;using System.Linq;";
    private const string _defaultClassName = "ViewModel";
    private static readonly ConcurrentDictionary<Type, bool> IsSimpleTypeCache = new ConcurrentDictionary<System.Type, bool>();
    private static bool IsNullableSimpleType(Type t)
    {
        var underlyingType = Nullable.GetUnderlyingType(t);
        return underlyingType != null && IsSimpleType(underlyingType);
    }
    private static bool IsSimpleType(Type type)
    {
        return IsSimpleTypeCache.GetOrAdd(type, t =>
            type.IsPrimitive ||
            type.IsEnum ||
            type == typeof(string) ||
            type == typeof(decimal) ||
            type == typeof(DateTime) ||
            type == typeof(DateTimeOffset) ||
            type == typeof(TimeSpan) ||
            type == typeof(Guid) ||
            IsNullableSimpleType(type));
    }
    private static bool IsGenericEnumerable(Type type)
    {
        return type.IsGenericType && type.GetInterfaces().Any(ti => (ti == typeof(IEnumerable<>) || ti.Name == "IEnumerable"));
    }
    private static bool IsEnumerable(Type type)
    {
        return IsGenericEnumerable(type) || type.IsArray;
    }

    public static void Run()
    {
        string includesPath = HttpContext.Current.Server.MapPath("~/_compt");

        string[] modules = Directory.GetFiles(includesPath, "*.cshtml");
        foreach (string module in modules)
        {
            string key = module.GetAfterLast("\\").GetBefore(".");
            string code = File.ReadAllText(module);

            var phrases = new Dictionary<string, string>();

            foreach (var part in code.Split(new string[] { "Text[" }, StringSplitOptions.None))
            {
                if (!part.StartsWith("\"") && !part.StartsWith("en")) continue;
                var segment = part.GetBefore("]");

                var en = segment.GetBefore(",").GetAfter("\"").GetBefore("\"");
                var vi = segment.GetAfter(",").GetAfter("\"").GetBefore("\"");

                if (!phrases.ContainsKey(en))
                    phrases.Add(en, vi);
            }

            string strViewModel = code
                .GetBefore("@{")
                .GetAfter("@functions")
                .GetAfter("{")
                .GetBeforeLast("}")
                .GetBefore("/*end_viewmodel*/")
                .Trim();

            if (string.IsNullOrEmpty(strViewModel)) { continue; }
            Sync(key, strViewModel, phrases);
        }
    }

    private static void Sync(string key, string code, IDictionary<string, string> phrases)
    {
        Type type = CompileClass(code);

        if (type == null)
        {
            return;
        }

        var comptAttrb = GetComponentAttribute(type);
        var jsonSchema = GetJsonSchema(type);
        var jsonDefaultValue = GetJsonDefaultValue(type, phrases);

        if (comptAttrb == null || jsonSchema == null)
        {
            return;
        }

        var compt = Root.Db.GetOne<PP_Compt>(t => t.ComptKey == key);

        if (compt != null)
        {
            compt.ComptKey = key;
            compt.ComptType = comptAttrb.Type;
            compt.ComptName = comptAttrb.ComptName;
            compt.JsonSchema = jsonSchema;
            compt.PathPostfix = comptAttrb.PathPostfix;
            compt.NodeType = comptAttrb.NodeType;
            compt.PageType = comptAttrb.PageType;

            Root.Db.Update<PP_Compt>(compt);
        }
        else
        {
            var newCompt = new PP_Compt();
            newCompt.ComptKey = key;
            newCompt.ComptType = comptAttrb.Type;
            newCompt.ComptName = comptAttrb.ComptName;
            newCompt.JsonSchema = jsonSchema;
            newCompt.JsonDefault = jsonDefaultValue;
            newCompt.PathPostfix = comptAttrb.PathPostfix;
            newCompt.NodeType = comptAttrb.NodeType;
            newCompt.PageType = comptAttrb.PageType;

            Root.Db.Insert<PP_Compt>(newCompt);
        }
    }

    private static Type CompileClass(string code)
    {
        code = _defautUsing + code;

        try
        {
            var options = new CompilerParameters();
            options.GenerateExecutable = false;
            options.GenerateInMemory = false;
            options.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(NameValueCollection)).Location);
            options.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(Attribute)).Location);
            options.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(FieldAttribute)).Location);
            options.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(Enumerable)).Location);
            options.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);

            var provider = new CSharpCodeProvider();
            var compile = provider.CompileAssemblyFromSource(options, code);

            return compile.CompiledAssembly.GetType(_defaultClassName);

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private static ComponentAttribute GetComponentAttribute(Type type)
    {
        return (ComponentAttribute)type
              .GetCustomAttributes(false)
              .Where(p => p.GetType() == typeof(ComponentAttribute))
              .FirstOrDefault();
    }

    private static string GetJsonSchema(Type type)
    {
        var schema = GetObjectSchema(type);
		var json = JsonConvert.SerializeObject(schema,
			Formatting.None,
			new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			});
        return json;
	}

    private static ComponentField GetField(PropertyInfo prop, FieldAttribute attrb)
    {
        string propName = prop.Name;

        var field = new ComponentField();
        field.FieldId = prop.Name;
        field.FieldTitle = attrb.Title;
        field.Required = attrb.Required;
        field.ControlType = attrb.Control;
        field.FieldTitle = attrb.Title;
        field.PlaceHolder = attrb.PlaceHolder;
        field.Options = attrb.Options;
        return field;
    }

    public static ObjectSchema GetObjectSchema(Type type, string propId = null, string title = null, string childTitle = null, int depth = 0)
    {
        var schema = new ObjectSchema();
        schema.Title = title ?? propId;
        schema.ChildTitle = childTitle ?? (!IsSimpleType(type) ? type.Name : string.Empty);
        schema.PropName = propId;

        foreach (var prop in type.GetProperties())
        {
            if (prop.IsStatic()) continue;

            var fieldAttrb = (FieldAttribute)prop
             .GetCustomAttributes(false)
             .Where(p => p.GetType() == typeof(FieldAttribute))
             .FirstOrDefault();

            if (fieldAttrb == null) continue;

            if (!IsEnumerable(prop.PropertyType))
            {
                if (IsSimpleType(prop.PropertyType))
                {
                    schema.SingleFieldTypes.Add(GetField(prop, fieldAttrb));
                }
                else
                {
                    if (depth <= 4)
                        schema.SingleObjectTypes.Add(GetObjectSchema(prop.PropertyType, prop.Name, fieldAttrb.Title, depth: depth + 1));
                }
            }
            else if (prop.PropertyType.IsArray)
            {
                Type arrayElementType = prop.PropertyType.GetElementType();

                if (IsSimpleType(arrayElementType))
                {
                    schema.ArrayFieldTypes.Add(GetField(prop, fieldAttrb));
                }
                else
                {
                    if (depth <= 4)
                        schema.ArrayObjectTypes.Add(GetObjectSchema(arrayElementType, prop.Name, fieldAttrb.Title ?? prop.Name, fieldAttrb.ChildTitle, depth: depth + 1));
                }
            }
            else
            {
                var elementType = prop.PropertyType.GetGenericArguments()[0];

                if (IsSimpleType(elementType))
                {
                    if (fieldAttrb == null) continue;
                    schema.ArrayFieldTypes.Add(GetField(prop, fieldAttrb));
                }
                else
                {
                    if (depth <= 4)
                        schema.ArrayObjectTypes.Add(GetObjectSchema(elementType, prop.Name, fieldAttrb.Title ?? prop.Name, fieldAttrb.ChildTitle, depth: depth + 1));
                }
            }
        }

        return schema;
    }

    private static string GetJsonDefaultValue(Type type, IDictionary<string, string> phrases)
    {
        var defaultProp = type.GetProperty("Default", BindingFlags.Public | BindingFlags.Static);

        if (defaultProp == null)
        {
            return null;
        }

        dynamic defaultValue = defaultProp.GetValue(null, null);
        defaultValue.Phrases = phrases;

        if (defaultValue == null)
        {
            return null;
        }

		return JsonConvert.SerializeObject(defaultValue,
			Formatting.None,
			new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			});
	}
}