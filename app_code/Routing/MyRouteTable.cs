using System.Web.Routing;
using System.Linq;
using System.Collections.Generic;

public static class MyRouteTable
{
    public static void RefreshRoutes(List<PP_Page> pages = null)
    {
        using (RouteTable.Routes.GetWriteLock())
        {
            pages = pages ?? Root.Db.GetList<PP_Page>().OrderByDescending(t => t.PathPattern).ToList();
            RouteTable.Routes.Clear();

            foreach (var p in pages)
            {
                var lang = Root.Langs[p.LangId];

                if (lang.Enabled == false)
                    continue;

                RouteTable.Routes.MapWebPageRoute(p.PathPattern, $"~/_compt/{p.ComptKey}.cshtml", new { LangId = p.LangId, PageId = p.Id });
                if(p.PathPattern.EndsWith("/{0}"))
                    RouteTable.Routes.MapWebPageRoute(p.PathPattern.GetBeforeLast("/"), $"~/_compt/{p.ComptKey}.cshtml", new { LangId = p.LangId, PageId = p.Id });
				
			}

			//System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
		}
    }
}