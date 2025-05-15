﻿using System;
using System.Linq;

public static class StringHelper
{
    public static string GetBefore(this string str, string key)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        int idx = str.IndexOf(key);

        if (idx > -1)
            return str.Substring(0, idx);
        else
            return str;
    }

    public static string GetBeforeLast(this string str, string key)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        int idx = str.LastIndexOf(key);

        if (idx > -1)
            return str.Substring(0, idx);
        else
            return str;
    }

    public static string GetAfter(this string str, string key)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        int idx = str.IndexOf(key);

        if (idx > -1)
            return str.Substring(idx + key.Length);
        else
            return str;
    }

    public static string GetAfterLast(this string str, string key)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        int idx = str.LastIndexOf(key);

        if (idx > -1)
            return str.Substring(idx + key.Length);
        else
            return str;
    }

    public static string NullIfWhiteSpace(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) { return null; }
        return value.Trim();
    }

    public static DateTime? NullIfDateTimeMin(this DateTime value)
    {
        if (value == DateTime.MinValue) { return null; }
        return value;
    }

    public static int? NullIfZero(this int value)
    {
        if (value == 0)
            return null;

        return value;
    }

    public static string RootAsEmpty(this string value)
    {
        if (value == "root" || value == "home" || value == "trang-chu")
            return string.Empty;

        return value.Trim();
    }

    public static int TryParseInt(this string value)
    {
        int r = 1;
        return int.TryParse(value, out r) ? r : 1;
    }

    public static string PathJoin(params string[] args)
    {
        return string.Join("/", args.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray());
    }
}