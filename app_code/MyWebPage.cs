using System;
using System.Web;
using System.Web.Helpers;
using System.Linq;
using System.Collections.Generic;
using System.Web.WebPages;
using System.IO;
using System.Web.WebPages.Instrumentation;

public class MyWebPage : System.Web.WebPages.WebPage
{
	public override void Execute() { }

    public override void Write(object value)
    {
        WriteLiteral(value);
    }

    public override void WriteAttributeTo(TextWriter writer, string name, PositionTagged<string> prefix, PositionTagged<string> suffix, params AttributeValue[] values)
    {
        WriteAttributeTo(VirtualPath, writer, name, prefix, suffix, values);
    }

    protected override void WriteAttributeTo(string pageVirtualPath, TextWriter writer, string name, PositionTagged<string> prefix, PositionTagged<string> suffix, params AttributeValue[] values)
    {
        bool first = true;
        bool wroteSomething = false;
        if (values.Length == 0)
        {
            // Explicitly empty attribute, so write the prefix and suffix
            WritePositionTaggedLiteral(writer, pageVirtualPath, prefix);
            WritePositionTaggedLiteral(writer, pageVirtualPath, suffix);
        }
        else
        {
            for (int i = 0; i < values.Length; i++)
            {
                AttributeValue attrVal = values[i];
                PositionTagged<object> val = attrVal.Value;
                PositionTagged<string> next = i == values.Length - 1 ?
                    suffix : // End of the list, grab the suffix
                    values[i + 1].Prefix; // Still in the list, grab the next prefix

                if (val.Value == null)
                {
                    // Nothing to write
                    continue;
                }

                // The special cases here are that the value we're writing might already be a string, or that the 
                // value might be a bool. If the value is the bool 'true' we want to write the attribute name instead
                // of the string 'true'. If the value is the bool 'false' we don't want to write anything.
                //
                // Otherwise the value is another object (perhaps an IHtmlString), and we'll ask it to format itself.
                string stringValue;

                // Intentionally using is+cast here for performance reasons. This is more performant than as+bool? 
                // because of boxing.
                if (val.Value is bool)
                {
                    if ((bool)val.Value)
                    {
                        stringValue = name;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    stringValue = val.Value as string;
                }

                if (first)
                {
                    WritePositionTaggedLiteral(writer, pageVirtualPath, prefix);
                    first = false;
                }
                else
                {
                    WritePositionTaggedLiteral(writer, pageVirtualPath, attrVal.Prefix);
                }

                // Calculate length of the source span by the position of the next value (or suffix)
                int sourceLength = next.Position - attrVal.Value.Position;

                BeginContext(writer, pageVirtualPath, attrVal.Value.Position, sourceLength, isLiteral: attrVal.Literal);

                // The extra branching here is to ensure that we call the Write*To(string) overload when
                // possible.
                if (attrVal.Literal && stringValue != null)
                {
                    WriteLiteralTo(writer, stringValue);
                }
                else if (attrVal.Literal)
                {
                    WriteLiteralTo(writer, val.Value);
                }
                else if (stringValue != null)
                {
                    if (val.Value is IHtmlString)
                        WriteTo(writer, val.Value);
                    else
                        WriteTo(writer, new HtmlString(stringValue));
                }
                else
                {
                    WriteTo(writer, val.Value);
                }

                EndContext(writer, pageVirtualPath, attrVal.Value.Position, sourceLength, isLiteral: attrVal.Literal);
                wroteSomething = true;
            }
            if (wroteSomething)
            {
                WritePositionTaggedLiteral(writer, pageVirtualPath, suffix);
            }
        }
    }

    private void WritePositionTaggedLiteral(TextWriter writer, string pageVirtualPath, string value, int position)
    {
        BeginContext(writer, pageVirtualPath, position, value.Length, isLiteral: true);
        WriteLiteralTo(writer, value);
        EndContext(writer, pageVirtualPath, position, value.Length, isLiteral: true);
    }

    private void WritePositionTaggedLiteral(TextWriter writer, string pageVirtualPath, PositionTagged<string> value)
    {
        WritePositionTaggedLiteral(writer, pageVirtualPath, value.Value, value.Position);
    }

    protected override void InitializePage()
	{
		if (IsDirectCall && !Context.IsDebuggingEnabled)
		{
			Response.StatusCode = 404;
			Response.End();
		}

		base.InitializePage();
	}

	protected IDataService Db => Root.Db;
	protected string LangId => Root.GetRouteData<string>("LangId") ?? "vi";
	protected int PageId => Root.GetRouteData<int>("PageId");
	protected string NodeSlug => HttpContext.Current.Request.Path.GetAfter("/").GetBefore("?");
	protected int CurrentPage => Request.QueryString["Page"].TryParseInt();
	protected bool IsDirectCall => VirtualPath.GetAfter("~/") == NodeSlug;
	protected bool IsPartialCall => this.VirtualPath != this.Request.AppRelativeCurrentExecutionFilePath;

	protected string FileName => VirtualPath.GetAfterLast("/").GetBefore(".");

	protected T LoadData<T>(string key, Func<T, T> setup = null) where T : class
	{
		var cache = WebCache.Get(key) as T;

		if (cache == null)
		{
			var config = Root.Configs.Values.FirstOrDefault(t => t.LangId == LangId && (t.PageId == PageId || t.PageId == 0) && t.ConfigKey == FileName);

			if (config != null)
			{
				cache = Json.Decode<T>(config.JsonContent);
			}

			if (setup != null)
				cache = setup(cache);

			if (cache == null)
			{
                if (HttpContext.Current.IsDebuggingEnabled)
                    return cache;

				Response.Write($"{key} not found!");
				Response.StatusCode = 404;
				Response.End();
			}

            if (HttpContext.Current.IsDebuggingEnabled)
                return cache;

            var expiration = DateTime.Now.AddMinutes(20);
			Root.CacheTable.AddOrUpdate(key, expiration, (k, e) => expiration);
			WebCache.Set(key, cache);
		}

		return cache;
	}
	protected string AntiForgeryTokenForAjaxPost()
	{
		var antiForgeryInputTag = AntiForgery.GetHtml().ToHtmlString();
		var removedStart = antiForgeryInputTag.Replace(@"<input name=""__RequestVerificationToken"" type=""hidden"" value=""", "");
		var tokenValue = removedStart.Replace(@""" />", "");

		if (antiForgeryInputTag == removedStart || removedStart == tokenValue)
			throw new InvalidOperationException("Oops! The Html.AntiForgeryToken() method seems to return something I did not expect.");

		return tokenValue;
	}
}