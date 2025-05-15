using MySql.Data.Entity;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Web;

public enum ProviderType
{
    SqlClient = 1,
    MySqlClient = 2
}

public static class DataServiceFactory
{
    private static ProviderType _dbProvider;
    private static string _conn;

    public static IDataService GetDbService()
    {
        if (_dbProvider == ProviderType.SqlClient)
        {
            return new MssqlDataService(_conn);
        }
        else if (_dbProvider == ProviderType.MySqlClient)
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            return new MysqlDataService(_conn);
        }

        return null;
    }

    public static void Init()
    {
        _conn = HttpContext.Current.IsDebuggingEnabled ? "dev" : "prod";
        var providerName = ConfigurationManager.ConnectionStrings[_conn].ProviderName.GetAfterLast(".");
        _dbProvider = (ProviderType)Enum.Parse(typeof(ProviderType), providerName);

    }
}