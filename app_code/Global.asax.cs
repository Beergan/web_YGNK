using FluentScheduler;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;

public class Global : HttpApplication
{
    private static System.Threading.Thread keepAliveThread = new System.Threading.Thread(KeepAlive);

    protected void Application_Start()
    {
        var t1 = DateTime.Now.Ticks;
        DataServiceFactory.Init();
        var db = DataServiceFactory.GetDbService();
      
        var dataSet = db.PP.QueryMultiple(@"SELECT * FROM pp_lang;SELECT * FROM pp_config;SELECT * FROM pp_page ORDER BY PathPattern DESC;SELECT * FROM pp_json");
        var langs = dataSet.Read<PP_Lang>().ToList();
        var configs = dataSet.Read<PP_Config>().ToList();
        var pages = dataSet.Read<PP_Page>().ToList();
        var jsons = dataSet.Read<PP_Json>().ToList();

        Root.ReloadLangs(langs);
        Root.RefreshConfigs(configs);
        MyRouteTable.RefreshRoutes(pages);
        Root.RefreshCategoryIndexes();

        jsons.ForEach(json =>
        {
            if (json.JsonKey == nameof(VisitCounter.UrlStats))
            {
                var urls = Json.Decode<Dictionary<string, int>>(json.JsonContent);
                foreach( var key in urls.Keys){
                    VisitCounter.UrlStats.AddOrUpdate(key, urls[key], (k, c) => urls[key] + c);
                }
            }
        });

        if (!HttpContext.Current.IsDebuggingEnabled) keepAliveThread.Start();

        Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory + "upload\\";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("log/index.html",
                outputTemplate: "[{Timestamp:yyMMdd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}<br/><br/>",
                rollingInterval: RollingInterval.Infinite,
                flushToDiskInterval: TimeSpan.FromSeconds(1)
            ).CreateLogger();

        JobManager.Initialize();
        JobManager.JobException += (exceptionInfo) => Log.Error(exceptionInfo.Exception, $"[job] {DateTime.Now:yyyyMMddHHmmss}");
        //JobManager.AddJob(() => VisitCounter.RefreshVisitStats(), s => s.NonReentrant().ToRunEvery(15).Minutes());
        //JobManager.AddJob(() => VisitCounter.RefreshVisitStats(), s => s.NonReentrant().ToRunEvery(1).Days().At(0, 0));
        var t2 = DateTime.Now.Ticks;
        Log.Information($"System boot {new TimeSpan(t2 - t1).TotalSeconds}");
    }

    protected void Application_End(object sender, EventArgs e)
    {
        var db = DataServiceFactory.GetDbService();
        db.Update(new PP_Json { JsonKey = nameof(VisitCounter.UrlStats), JsonContent = Json.Encode(VisitCounter.UrlStats) });
    }

    private static void KeepAlive()
    {
        string website = ConfigurationManager.AppSettings["website"];

        while (true)
        {
            try
            {
                var req = System.Net.WebRequest.Create(website);
                req.GetResponse();
                System.Threading.Thread.Sleep(60000);
            }
            catch (System.Threading.ThreadAbortException)
            {
                break;
            }
        }
    }

    protected void Application_Error(object sender, EventArgs e)
    {
        HttpException lastErrorWrapper = Server.GetLastError() as HttpException;

        Exception lastError = lastErrorWrapper;
        if (lastErrorWrapper.InnerException != null)
            lastError = lastErrorWrapper.InnerException;

        if (lastError == null) return;

        var context = HttpContext.Current;
        string url = string.Empty;

        try
        {
            url = context.Request.RawUrl;
        }
        catch
        {
            url = "[internal]";
        }

        Log.Error(lastError, url);
    }

    protected void Session_Start(object sender, EventArgs e)
    {
        try
        {
            VisitCounter.OnSessionStart_New();
        }
        catch (Exception ex)
        {
            Log.Error(ex, nameof(VisitCounter.OnSessionStart_New));
        };
    }

    protected void Application_AcquireRequestState(object sender, EventArgs e)
    {
        try
        {
            VisitCounter.OnRequestBegin_New();
        }
        catch (Exception ex)
        {
            Log.Error(ex, nameof(VisitCounter.OnRequestBegin_New));
        };
    }

    protected void Session_End(object sender, EventArgs e)
    {
        try
        {
            VisitCounter.OnSessionEnd_New(this.Session);
        }
        catch (Exception ex)
        {
            Log.Error(ex, nameof(VisitCounter.OnSessionEnd_New));
        };
    }
}