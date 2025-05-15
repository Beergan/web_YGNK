using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

public interface IDataService
{
    PetaPoco.Database PP { get; }
    DbContext EF { get; }

    T Insert<T>(T model) where T : class;
    T Update<T>(T model) where T : class;
    bool Delete<T>(object key) where T : class;
    bool Exists<T>(object key) where T : class;
    bool Exists<T>(Expression<Func<T, bool>> query) where T : class;
    T GetOne<T>(object key) where T : class;
    T GetOne<T>(Expression<Func<T, bool>> query) where T : class;
    List<T> GetList<T>(string query) where T : class;
    List<T> GetList<T>(Expression<Func<T, bool>> query = null) where T : class;
    IQueryable<T> Query<T>(Expression<Func<T, bool>> query) where T : class;
    IQueryable<T> Query<T>() where T : class;
    DbSet<T> Table<T>() where T : class;

    List<CategoryIndexer> GetCategoryIndexes();
    List<Tuple<string, string>> GetLinks(string langId);
    List<PP_Category> GetCategoryMenu(string langId, string nodeType = null);
    void RefreshVisitStats(DateTime now);
    PetaPoco.IGridReader GetDashboardData();
}