﻿using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

public class Field
{
    public bool Required { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public string Prompt { get; set; }
}

public class FieldExtractor<T> where T : class
{
    private static ConcurrentDictionary<string, DisplayAttribute> _dict = new ConcurrentDictionary<string, DisplayAttribute>();

    private string GetCorrectPropertyName(Expression<Func<T, object>> expression)
    {
        if (expression.Body is MemberExpression)
        {
            return ((MemberExpression)expression.Body).Member.Name;
        }
        else
        {
            var op = ((UnaryExpression)expression.Body).Operand;
            return ((MemberExpression)op).Member.Name;
        }
    }

    public FieldExtractor()
    {
        if (_dict.Count > 0)
            return;

        string typeName = typeof(T).Name;
        var props = typeof(T).GetProperties();

        foreach (PropertyInfo prop in props)
        {
            DisplayAttribute attrb = prop
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(DisplayAttribute))
                .FirstOrDefault() as DisplayAttribute;

            if (attrb == null) continue;
            string propName = prop.Name;

            if (_dict.ContainsKey(propName))
            {
                _dict[propName] = attrb;
            }
            else
            {
                _dict.TryAdd(propName, attrb);
            }
        }
    }

    public string GetName(Expression<Func<T, object>> field)
    {
        string fieldName = GetCorrectPropertyName(field);
        return fieldName;
    }

    public string GetTitle(Expression<Func<T, object>> field)
    {
        string propName = GetName(field);

        if (_dict.ContainsKey(propName))
        {
            return _dict[propName].Name;
        }

        return propName;
    }

    public string GetPrompt(Expression<Func<T, object>> field)
    {
        string propName = GetName(field);

        if (_dict.ContainsKey(propName))
        {
            return _dict[propName].Prompt;
        }

        return propName;
    }

    private Field GetField(Expression<Func<T, object>> field)
    {
        string propName = GetName(field);

        if (_dict.ContainsKey(propName))
        {
            var item = _dict[propName];

            return new Field
            {
                Name = propName,
                Title = item.Name,
                Prompt = item.Prompt
            };
        }

        return new Field { Name = propName, Title = propName };
    }

    public Field this[Expression<Func<T, object>> field]
    {
        get { return GetField(field); }
    }
}