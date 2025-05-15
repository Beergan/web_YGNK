using System;
using System.Collections.Generic;
using System.Linq;
using PetaPoco;

public class MssqlDataService : BaseDataService, IDataService, IDisposable
{
    public MssqlDataService(string conn) : base(conn) { }

    public void Dispose()
    {
        if (_pp != null) _pp.Dispose();
        if (_ef != null) _ef.Dispose();
    }

    public List<ComptWithLangs> Compt_GetListIncludeLangs()
    {
        return this.PP.Fetch<ComptWithLangs>(@"SELECT *, Langs = STUFF((SELECT ', ' + c.[LangId] FROM [pp_config] c WHERE main.[ComptKey] = c.[ConfigKey] ORDER BY c.[LangId] FOR XML PATH('')), 1, 2, '') FROM [pp_compt] main WHERE ComptKey LIKE 'footer%'");
    }

    public List<CategoryIndexer> GetCategoryIndexes()
    {
        return this.PP.Fetch<CategoryIndexer>(@"WITH cat (RootId, CatId)
                AS (
	                SELECT RootId = Id, CatId = Id
	                FROM pp_category c1
	                UNION ALL
	                SELECT RootId = cat.RootId, CatId = Id
	                FROM pp_category c2
	                INNER JOIN cat ON c2.ParentId = cat.CatId
	                )
                SELECT RootId, Array = STUFF((
			                SELECT ',' + CAST(CatId AS VARCHAR(20))
			                FROM cat sub
			                WHERE main.RootId = sub.RootId
			                FOR XML PATH('')
			                ), 1, 1, '')
                FROM cat main
                GROUP BY RootId");
    }
    public List<Tuple<string, string>> GetLinks(string langId)
    {
        return this.PP.Fetch<dynamic>(@"SELECT Title, Url = '/' + PathPattern
            FROM pp_page
            WHERE LangId= @0 AND (PageType IS NULL OR PageType NOT IN ('item','list'))
            UNION ALL
            SELECT Title, Url = '/' + CategoryPath
            FROM pp_category WHERE LangId= @0
            UNION ALL
            SELECT Title, Url = '/' + NodePath
            FROM pp_node WHERE LangId = @0
            UNION ALL
            SELECT Title, Url = '/' + NodePath
            FROM pp_product WHERE LangId = @0", langId)
            .Select(t => new Tuple<string, string>(t.Title, t.Url))
            .ToList();
    }
    public void RefreshVisitStats(DateTime date)
    {
        int sevenDaysAgo = int.Parse($"{DateTime.Now.AddDays(-7):yyyyMMdd}");
        PP.Delete<PP_Stats_Daily>("WHERE [Date] < @0", sevenDaysAgo);

        int now = int.Parse($"{DateTime.Now.AddHours(-4):yyyyMMdd}");
        string query = @"SELECT @0 [Date],
            (SELECT COUNT(1) FROM pp_visit WHERE [Date] = @0) VisitCount,
            (SELECT COUNT(1) FROM pp_order WHERE [Date] = @0) OrderCount";

        var visitStatsToday =  PP.FirstOrDefault<PP_Stats_Daily>(query, now);

        if (Exists<PP_Stats_Daily>(t => t.Date == visitStatsToday.Date))
            PP.Update(visitStatsToday);
        else
            PP.Insert(visitStatsToday);
    }

    public IGridReader GetDashboardData()
    {
        return PP.QueryMultiple(
@"SELECT TOP 5 * FROM pp_visit ORDER BY Date DESC;
SELECT TOP 5 * FROM pp_order ORDER BY Date DESC");
    }

    public List<PP_Category> GetCategoryMenu(string langId, string nodeType)
    {
        throw new NotImplementedException();
    }
}