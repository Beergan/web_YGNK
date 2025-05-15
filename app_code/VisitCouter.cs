using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.SessionState;

public static class VisitCounter
{
	public static ConcurrentDictionary<string, PP_Visit> OnlineSessions = new ConcurrentDictionary<string, PP_Visit>();
	public static ConcurrentDictionary<string, int> UrlStats = new ConcurrentDictionary<string, int>();
	private readonly static List<string> MacosPlatforms = new List<string> { "Macintosh", "MacIntel", "MacPPC", "Mac68K" };
	private readonly static List<string> WindowsPlatforms = new List<string> { "Win32", "Win64", "Windows", "WinCE" };
	private readonly static List<string> IosPlatforms = new List<string> { "iPhone", "iPad", "iPod" };

	public static string GetDeviceName(string agent)
	{
		if (agent == null) return "NA";

		if (MacosPlatforms.Any(t => agent.Contains(t)))
			return "Mac OS";
		if (IosPlatforms.Any(t => agent.Contains(t)))
			return "Ios";
		if (WindowsPlatforms.Any(t => agent.Contains(t)))
			return "Windows";
		if (agent.Contains("Android"))
			return "Android";
		if (agent.Contains("Linux"))
			return "Linux";

		return "NA";
	}

	public static void OnSessionStart()
	{
		var firstTicks = DateTime.Now.Ticks;
		HttpContext.Current.Session[nameof(firstTicks)] = firstTicks;
	}

	public static void OnRequestPing()
	{
		var heartBeatTicks = DateTime.Now.Ticks;
		HttpContext.Current.Session[nameof(heartBeatTicks)] = heartBeatTicks;
	}

	public static void OnRequestBegin()
	{
		var currentUrl = HttpContext.Current.Request.RawUrl;
		var session = HttpContext.Current.Session;
		var request = HttpContext.Current.Request;

		if (session == null || HttpContext.Current.Request.HttpMethod == HttpMethod.Post.Method)
			return;

		string lastUrl = (string)session[nameof(lastUrl)];
		if (currentUrl == lastUrl || currentUrl.EndsWith(".ashx") || currentUrl.StartsWith("/admin"))
			return;

		long currentTicks = DateTime.Now.Ticks;
		long lastTicks = (long)(session[nameof(lastTicks)] ?? 0L);

		session[nameof(lastUrl)] = currentUrl;
		session[nameof(lastTicks)] = currentTicks;

		List<KeyValuePair<string, long>> visitHistory = (session[nameof(visitHistory)] ?? (session[nameof(visitHistory)] = new List<KeyValuePair<string, long>>())) as List<KeyValuePair<string, long>>;

		if (lastTicks > 0)
		{
			visitHistory.Add(new KeyValuePair<string, long>(lastUrl, (long)new TimeSpan(currentTicks - lastTicks).TotalSeconds));
		}
		else
		{
			var ipAddress = request.ServerVariables["HTTP_CF_CONNECTING_IP"];
			var browserName = request.Browser.Browser + request.Browser.Version;
			var device = GetDeviceName(request.UserAgent);
			session[nameof(ipAddress)] = ipAddress;
			session[nameof(browserName)] = browserName;
			session[nameof(device)] = device;
		}
	}

	public static void OnSessionEnd(HttpSessionState session)
	{
		long heartBeatTicks = (long)(session[nameof(heartBeatTicks)] ?? 0L);
		long lastTicks = (long)(session[nameof(lastTicks)] ?? 0L);

		if (heartBeatTicks == 0 || lastTicks == 0)
			return;

		long firstTicks = (long)session[nameof(firstTicks)];
		string lastUrl = (string)session[nameof(lastUrl)];

		List<KeyValuePair<string, long>> visitHistory = session[nameof(visitHistory)] as List<KeyValuePair<string, long>>;
		visitHistory.Add(new KeyValuePair<string, long>(lastUrl, (long)new TimeSpan(heartBeatTicks - lastTicks).TotalSeconds));

		string country = (string)session[nameof(country)] ?? "NA";
		string province = (string)session[nameof(province)] ?? "NA";
		string ipAddress = session[nameof(ipAddress)] as string;
		string device = session[nameof(device)] as string;
		string browserName = session[nameof(browserName)] as string;
		var stayTime = (long)new TimeSpan(heartBeatTicks - firstTicks).TotalSeconds;
		bool makeOrder = (bool)(session[nameof(makeOrder)] ?? false);

		visitHistory
			.GroupBy(v => v.Key)
			.Select(g => g.Key).ToList()
			.ForEach(url => UrlStats.AddOrUpdate(url, 1, (u, counter) => counter + 1));

		PP_Visit visit = new PP_Visit();
		visit.SessionId = $"{DateTime.Now:yyMMddHHmmss}{shortid.ShortId.Generate(useNumbers: false, useSpecial: false, length: 8)}"; ;
		visit.Date = int.Parse($"{DateTime.Now:yyyyMMdd}");
		visit.Country = country;
		visit.Referer = province;
		visit.Ip = ipAddress;
		visit.Device = device;
		visit.Browser = browserName;
		visit.StayTime = (int)stayTime;
		visit.ClickCount = visitHistory.Count;
		visit.MakeOrder = makeOrder;
		visit.JsonDetails = Json.Encode(visitHistory);
		DataServiceFactory.GetDbService().Insert<PP_Visit>(visit);
	}

	public static void OnSessionStart_New()
	{
		var session = HttpContext.Current.Session;
		PP_Visit visitData = (PP_Visit)(session[nameof(visitData)]
								?? (session[nameof(visitData)] = new PP_Visit()));

		visitData.FirstTick = DateTime.Now.Ticks;
		visitData.Referer = (HttpContext.Current.Request?.UrlReferrer?.Host) ?? "NA";
	}

	public static void OnRequestPing_New()
	{
		var session = HttpContext.Current.Session;
		PP_Visit visitData = (PP_Visit)session[nameof(visitData)];
		if (visitData == null) return;

		visitData.HeartBeat = DateTime.Now.Ticks;
	}

	public static void OnRequestBegin_New()
	{
		var session = HttpContext.Current.Session;
		if (session == null || HttpContext.Current.Request.HttpMethod == HttpMethod.Post.Method)
			return;

		PP_Visit visitData = (PP_Visit)session[nameof(visitData)];
		if (visitData == null) return;

		var request = HttpContext.Current.Request;
		var currentUrl = request.RawUrl;

		if (currentUrl == visitData.LastUrl
			|| currentUrl.EndsWith(".ashx")
			|| currentUrl.StartsWith("/admin"))
			return;

		long currentTicks = DateTime.Now.Ticks;
		if (visitData.LastTick > 0)
		{
			var stayTime = (long)new TimeSpan(currentTicks - visitData.LastTick).TotalSeconds;
			visitData.Urls.Add(new KeyValuePair<string, long>(visitData.LastUrl, stayTime));
		}
		else
		{
			visitData.SessionId = $"{DateTime.Now:yyMMddHHmmss}_{session.SessionID}";
			visitData.Ip = request.ServerVariables["HTTP_CF_CONNECTING_IP"]; ;
			visitData.Browser = request.Browser.Browser + request.Browser.Version; ;
			visitData.Device = GetDeviceName(request.UserAgent);
			visitData.Urls = new List<KeyValuePair<string, long>>();
			OnlineSessions[session.SessionID] = visitData;
			visitData.Date = int.Parse($"{DateTime.Now:yyyyMMdd}");
			DataServiceFactory.GetDbService().Insert<PP_Visit>(visitData);

			var dayly = new PP_Stats_Daily();
			var item = DataServiceFactory.GetDbService().GetOne<PP_Stats_Daily>(x => x.Date == visitData.Date);
			if (item != null)
			{
				item.VisitCount++;
				DataServiceFactory.GetDbService().Update<PP_Stats_Daily>(item);
			}
			else
			{
				dayly.Date = visitData.Date;
				dayly.VisitCount = 1;
				DataServiceFactory.GetDbService().Insert<PP_Stats_Daily>(dayly);
			}
		}

		visitData.LastUrl = currentUrl;
		visitData.LastTick = currentTicks;
	}

	public static void OnSessionEnd_New(HttpSessionState session)
	{
		PP_Visit visitData = (PP_Visit)session[nameof(visitData)];
		if (visitData == null
			|| visitData.HeartBeat == 0
			|| visitData.LastTick == 0)
		{
			PP_Visit removedValue;
			OnlineSessions.TryRemove(session.SessionID, out removedValue);
			return;
		}

		try
		{
			var stayTime = (long)new TimeSpan(visitData.HeartBeat - visitData.LastTick).TotalSeconds;
			visitData.Urls.Add(new KeyValuePair<string, long>(visitData.LastUrl, stayTime));
			visitData.Date = int.Parse($"{DateTime.Now:yyyyMMdd}");
			visitData.StayTime = (int)new TimeSpan(visitData.HeartBeat - visitData.FirstTick).TotalSeconds;
			visitData.ClickCount = visitData.Urls.Count;
			visitData.MakeOrder = false;
			visitData.JsonDetails = Json.Encode(visitData.Urls);
			visitData.Country = "NA";
			visitData.Referer = "NA";

			visitData.Urls
				.GroupBy(v => v.Key)
				.Select(g => g.Key).ToList()
				.ForEach(url => UrlStats.AddOrUpdate(url, 1, (u, counter) => counter + 1));

			DataServiceFactory.GetDbService().Insert<PP_Visit>(visitData);

			PP_Visit removedValue;
			OnlineSessions.TryRemove(session.SessionID, out removedValue);
		}
		catch (Exception ex)
		{
			// Log the exception to troubleshoot
			System.Diagnostics.Debug.WriteLine($"Error during session end: {ex.Message}");
		}
	}


	public static void RefreshVisitStats()
	{
		var db = DataServiceFactory.GetDbService();
		db.RefreshVisitStats(DateTime.Now);
		db.Update(new PP_Json { JsonKey = nameof(VisitCounter.UrlStats), JsonContent = Json.Encode(VisitCounter.UrlStats) });
	}
}