using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace KRBAccounting.Data.CacheRepo
{
    public class SqlDependancyCacheProvider : ICacheProvider
    {
        private Cache _cache;
        public string DbEntryName { get; set; }
        public string TableName { get; set; }
        public string ConnString = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;

        public SqlDependancyCacheProvider(string dbEntryName, string tableName)
        {
            _cache = System.Web.HttpRuntime.Cache;
            DbEntryName = dbEntryName;
            TableName = tableName;
        }

        public object Get(string key)
        {
            return _cache[key];
        }

        public void Set(string key, object data, int cacheTime)
        {
            try
            {
                SqlCacheDependency dep = new SqlCacheDependency(DbEntryName, TableName);
                _cache.Add(key, data, dep, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTime), System.Web.Caching.CacheItemPriority.Normal, null);
            }
            catch (DatabaseNotEnabledForNotificationException exDBDis)
            {
                try
                {
                    SqlCacheDependencyAdmin.EnableNotifications(ConnString);
                    SqlCacheDependencyAdmin.EnableTableForNotifications(ConnString, TableName);
                    SqlCacheDependency dep = new SqlCacheDependency(DbEntryName, TableName);
                    _cache.Add(key, data, dep, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTime), System.Web.Caching.CacheItemPriority.Normal, null);
                }
                catch (UnauthorizedAccessException exPerm)
                {
                    HttpContext.Current.Response.Redirect("Error.cshtml");
                }
            }
            catch (TableNotEnabledForNotificationException exTabDis)
            {
                try
                {
                    SqlCacheDependencyAdmin.EnableTableForNotifications(ConnString, TableName);
                    SqlCacheDependency dep = new SqlCacheDependency(DbEntryName, TableName);
                    _cache.Add(key, data, dep, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTime), System.Web.Caching.CacheItemPriority.Normal, null);
                }
                catch (System.Data.SqlClient.SqlException exc)
                {
                    HttpContext.Current.Response.Redirect("Error.cshtml");
                }
            }
        }

        public bool IsSet(string key)
        {
            return (_cache[key] != null);
        }

        public void Invalidate(string key)
        {
            _cache.Remove(key);
        }
    }
}