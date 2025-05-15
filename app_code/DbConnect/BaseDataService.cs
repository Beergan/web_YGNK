using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

public class BaseDataService
{
    protected string _conn;
    protected PetaPoco.Database _pp = null;
    protected MyDbContext _ef = null;

    public BaseDataService(string conn)
    {
        _conn = conn;
    }

    public PetaPoco.Database PP => _pp ?? (_pp = new PetaPoco.Database(_conn));
    public DbContext EF => _ef ?? (_ef = new MyDbContext(_conn));

    public bool Exists<T>(object key) where T : class
    {
        return this.PP.Exists<T>(key);
    }
    public bool Exists<T>(Expression<Func<T, bool>> query) where T : class
    {
        return this.EF
            .Set<T>()
            .AsNoTracking()
            .Any(query);
    }
    public T GetOne<T>(object key) where T : class
    {
        return this.PP.SingleOrDefault<T>(key);
    }
    public T GetOne<T>(Expression<Func<T, bool>> query) where T : class
    {
        return this.EF
            .Set<T>()
            .AsNoTracking()
            .FirstOrDefault(query);
    }
    public List<T> GetList<T>(string query) where T : class
    {
        return this.PP.Fetch<T>(query);
    }
    public List<T> GetList<T>(Expression<Func<T, bool>> query = null) where T : class
    {
        if (query != null)
        {
            return this.EF.Set<T>().AsNoTracking().Where(query).ToList();
        }
        else
        {
            return this.EF.Set<T>().AsNoTracking().ToList();
        }
    }
    public IQueryable<T> Query<T>(Expression<Func<T, bool>> query) where T : class
    {
        return this.EF.Set<T>().AsNoTracking().Where(query);
    }
    public IQueryable<T> Query<T>() where T : class
    {
        return this.EF.Set<T>().AsNoTracking();
    }
    public T Insert<T>(T model) where T : class
    {
        return this.PP.Insert(model) as T;
    }
    public T Update<T>(T model) where T : class
    {
        if (this.PP.Update(model) > 0)
            return model;
        else
            return null;
    }
    public bool Delete<T>(object key) where T : class
    {
        var itemToDelete = this.PP.Single<T>(key);

        if (itemToDelete == null)
            return false;

        if (this.PP.Delete<T>(key) > 0)
            return true;
        else
            return false;
    }
    public DbSet<T> Table<T>() where T : class => this.EF.Set<T>();
}