using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using PetaPoco;

public class MysqlDataService : BaseDataService, IDataService, IDisposable
{
    public MysqlDataService(string conn) : base(conn) { }

    public void Dispose()
    {
        if (_pp != null) _pp.Dispose();
        if (_ef != null) _ef.Dispose();
    }

    public List<ComptWithLangs> Compt_GetListIncludeLangs()
    {
        return this.PP.Fetch<ComptWithLangs>(@"SELECT *, (SELECT GROUP_CONCAT(DISTINCT c.LangId) FROM pp_config c WHERE main.ComptKey = c.ConfigKey ORDER BY c.LangId) AS Langs FROM pp_compt main WHERE ComptKey LIKE 'layout%'");
    }
    public List<CategoryIndexer> GetCategoryIndexes()
    {
        using (var db = new PetaPoco.Database(ConfigurationManager.ConnectionStrings[_conn].ConnectionString + "Allow User Variables=True;", "MySql"))
        {
            db.EnableNamedParams = false;
            var query = "SELECT @pv:=r.Id, Id RootId, (SELECT GROUP_CONCAT(`Id` SEPARATOR ',') FROM (SELECT Id, ParentId FROM pp_category) node WHERE (Id = @pv OR FIND_IN_SET(ParentId, @pv) > 0) AND @pv := CONCAT(@pv, ',', Id)) Array FROM pp_category r";

            return db.Fetch<CategoryIndexer>(query);
        }
    }

    public List<PP_Category> GetCategoryMenu(string langId, string nodeType = null)
    {
        using (var db = new PetaPoco.Database(ConfigurationManager.ConnectionStrings[_conn].ConnectionString + "Allow User Variables=True;", "MySql"))
        {
            db.EnableNamedParams = false;
            var query = @"SELECT l.* FROM (SELECT @pv:=CONCAT('-', COALESCE(r.ParentId, r.Id), '-'), Id, (SELECT 1 FROM pp_category x WHERE (FIND_IN_SET(CONCAT('-', Id, '-'), @pv)) > 0 AND @pv:= CASE WHEN x.ParentId IS NOT NULL THEN CONCAT('-', x.ParentId, @pv) ELSE @pv END), CONCAT(@pv, CASE WHEN CategoryLevel > 1 THEN r.Id ELSE '' END) Path FROM pp_category r) k INNER JOIN pp_category l ON k.Id = l.Id WHERE LangId = @0 ORDER BY k.Path";

            return db.Fetch<PP_Category>(query, langId);
        }
    }
    public List<Tuple<string, string>> GetLinks(string langId)
    {
        return this.PP.Fetch<dynamic>(@"SELECT Title, CONCAT('/', PathPattern) AS Url
            FROM pp_page
            WHERE LangId= @0 AND (PageType IS NULL OR PageType NOT IN ('item','list'))
            UNION ALL
            SELECT Title, CONCAT('/', CategoryPath) AS Url
            FROM pp_category WHERE LangId= @0
            UNION ALL
            SELECT Title, CONCAT('/', NodePath) AS Url
            FROM pp_node WHERE LangId= @0
            UNION ALL
            SELECT Title, CONCAT('/', NodePath) AS Url
            FROM pp_product WHERE LangId= @0", langId)
            .Select(t => new Tuple<string, string>(t.Title, t.Url))
            .ToList();
    }
    public void RefreshVisitStats(DateTime date)
    {
        int sevenDaysAgo = int.Parse($"{DateTime.Now.AddDays(-7):yyyyMMdd}");
        PP.Delete<PP_Stats_Daily>("WHERE `Date` < @0", sevenDaysAgo);

        int now = int.Parse($"{DateTime.Now.AddHours(-4):yyyyMMdd}");
        string query = @"SELECT @0 `Date`,
            (SELECT COUNT(1) FROM pp_visit WHERE `Date` = @0) VisitCount,
            (SELECT COUNT(1) FROM pp_order WHERE `Date` = @0) OrderCount";

        var stats = this.PP.FirstOrDefault<PP_Stats_Daily>(query, now);

        if (Exists<PP_Stats_Daily>(stats.Date))
            PP.Update(stats);
        else
            PP.Insert(stats);
    }
    public IGridReader GetDashboardData()
    {
        return PP.QueryMultiple(
@"SELECT * FROM pp_visit ORDER BY Date DESC LIMIT 5;
SELECT * FROM pp_order ORDER BY Date DESC LIMIT 5");
    }
}