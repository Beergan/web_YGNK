using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;

public static class Root
{
    public static ConcurrentDictionary<string, DateTime> CacheTable = new ConcurrentDictionary<string, DateTime>();
    public static ConcurrentDictionary<string, PP_Lang> Langs = new ConcurrentDictionary<string, PP_Lang>();
    public static ConcurrentDictionary<string, PP_Config> Configs = new ConcurrentDictionary<string, PP_Config>();
    public static ConcurrentDictionary<int, int[]> CategoryIndexes = new ConcurrentDictionary<int, int[]>();

    public static T GetRouteData<T>(string key)
    {
        var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
        return routeValues.ContainsKey(key) ? (T)routeValues[key] : default(T);
    }

    public static T GetContextData<T>(string key, Func<T> setup)
    {
        var items = HttpContext.Current.Items;
        return (T)(items[key] ?? (items[key] = setup()));
    }

    public static string GetCookieData(string key, Func<string> setup)
    {
        var cookies = HttpContext.Current.Request.Cookies;
        var value = cookies.AllKeys.Contains(key) && !string.IsNullOrEmpty(cookies[key].Value) ? cookies[key].Value : null;
        return (value ?? (value = setup()));
    }

    public static IDataService Db
    {
        get
        {
            return GetContextData<IDataService>(nameof(IDataService), setup: () => DataServiceFactory.GetDbService());
        }
    }

    public static void ReloadLangs(List<PP_Lang> langs = null)
    {
        langs = langs ?? Db.GetList<PP_Lang>();
        langs.ForEach(i => Langs.AddOrUpdate(i.LangId, i, (a, b) => i));
    }

    public static void RefreshConfigs(List<PP_Config> configs = null)
    {
        configs = configs ?? Db.GetList<PP_Config>();
        configs.ForEach(t => Configs.AddOrUpdate($"{t.LangId}-{t.PageId}-{t.ConfigKey}", t, (a, b) => t));

        foreach (var key in CacheTable.Keys)
            WebCache.Remove(key);

        _rootConfig = null;
    }
    private static string LangId => GetRouteData<string>("LangId") ?? "vi";
    private static Dictionary<string, Config> _rootConfig = new Dictionary<string, Config>();
    public static Config Config
    {
        get
        {
            if (_rootConfig == null)
            {
                _rootConfig = new Dictionary<string, Config>() { { "vi", null }, { "en", null } };
            }

            return _rootConfig[LangId] ?? (_rootConfig[LangId] = Configs.ContainsKey($"{LangId}-0-root") ? Json.Decode<Config>(Configs[$"{LangId}-0-root"].JsonContent) : new Config());
        }
    }

    public static void RefreshCategoryIndexes()
    {
        var array = Db.GetCategoryIndexes();
        //array
        //.ForEach(t => CategoryIndexes
        //    .AddOrUpdate(
        //        t.RootId,
        //        t.Array.Split(','),
        //        (a, b) => t.Array.Split(',')
        //    )
        //);
        array
            .ForEach(t => CategoryIndexes
                .AddOrUpdate(
                    t.RootId,
                    Array.ConvertAll<string, int>(t.Array.Split(','), int.Parse),
                    (a, b) => Array.ConvertAll<string, int>(t.Array.Split(','), int.Parse)
                 )
            );
    }

    public static Dictionary<string, object> ToDictionary(this NameValueCollection @this)
    {
        var dict = new Dictionary<string, object>();

        if (@this != null)
        {
            foreach (string key in @this.AllKeys)
            {
                dict.Add(key, @this[key]);
            }
        }

        return dict;
    }

    //public static bool TryUpdateModel<TModel>(TModel model, IDictionary<string, object> values, System.Web.Mvc.ModelStateDictionary modelState) where TModel : class
    //{
    //    var binder = new System.Web.Mvc.DefaultModelBinder();
    //    var vp = new System.Web.Mvc.DictionaryValueProvider<object>(values, CultureInfo.CurrentCulture);
    //    var bindingContext = new System.Web.Mvc.ModelBindingContext
    //    {
    //        ModelMetadata = System.Web.Mvc.ModelMetadataProviders.Current.GetMetadataForType(() => model, typeof(TModel)),
    //        ModelState = modelState,
    //        PropertyFilter = propertyName => true,
    //        ValueProvider = vp
    //    };

    //    var ctx = new ControllerContext();
    //    binder.BindModel(ctx, bindingContext);
    //    return modelState.IsValid;
    //}

    public static void ClearCache()
    {
        foreach (var key in CacheTable.Keys)
        {
            DateTime value;
            WebCache.Remove(key);
            CacheTable.TryRemove(key, out value);
        }
    }

    public static string ShortId()
    {
        var ordercode = shortid.ShortId.Generate();

        return ordercode;
    }

    public static string AntiForgeryTokenForAjaxPost()
    {
        var antiForgeryInputTag = AntiForgery.GetHtml().ToHtmlString();
        var removedStart = antiForgeryInputTag.Replace(@"<input name=""__RequestVerificationToken"" type=""hidden"" value=""", "");
        var tokenValue = removedStart.Replace(@""" />", "");

        if (antiForgeryInputTag == removedStart || removedStart == tokenValue)
            throw new InvalidOperationException("Oops! The Html.AntiForgeryToken() method seems to return something I did not expect.");

        return tokenValue;
    }

    public static string GetMomentAgo(this DateTime date)
    {
        var delta = DateTime.Now - date;
        if (delta < TimeSpan.Zero)
            delta = TimeSpan.Zero;
        var pre = GetMomentsAgoUnit(delta);
        var unitCount = (int)(delta.TotalSeconds / pre.Item1.TotalSeconds);

        string result;
        if (unitCount == 0 && pre.Item1 == TimeSpan.FromSeconds(1))
            result = $"ngay bây giờ";
        else
        {
            result = $"{unitCount} {pre.Item2} trước";
        }

        return result;
    }

    private static Tuple<TimeSpan, string> GetMomentsAgoUnit(TimeSpan delta)
    {
        if (delta.TotalSeconds < 60)
            return new Tuple<TimeSpan, string>(TimeSpan.FromSeconds(1), "giây");
        if (delta.TotalMinutes < 60)
            return new Tuple<TimeSpan, string>(TimeSpan.FromMinutes(1), "phút");
        if (delta.TotalHours < 24)
            return new Tuple<TimeSpan, string>(TimeSpan.FromHours(1), "giờ");
        if (delta.TotalDays < 7)
            return new Tuple<TimeSpan, string>(TimeSpan.FromDays(1), "ngày");

        return new Tuple<TimeSpan, string>(TimeSpan.FromDays(7), "tuần");
    }

    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
    {
        return source ?? Enumerable.Empty<T>();
    }

    public static string CurrentWebsiteUrl => ConfigurationManager.AppSettings["website"];

    public static string LayoutId => ConfigurationManager.AppSettings["layout_id"];

    public static bool IsTrueValue(object obj)
    {
        if (obj == null) return false;
        return (bool)obj;
    }

    public static T DecodeJson<T>(string json)
    {
        try
        {
            return Json.Decode<T>(json);
        }
        catch
        {
            return default(T);
        }
    }
}