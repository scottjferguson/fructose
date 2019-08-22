using AutoMapper;
using Core.Caching;
using Core.Data;
using Core.Plugins.AutoMapper.Data.LookupData;
using Core.Plugins.AutoMapper.Data.Resolvers.Base;
using Core.Plugins.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Customer.Microservice.Map
{
    public class LookupDataValueToKeyDatabaseResolverFructose<T> : LookupDataResolverValueToKeyBaseFructose<T>
    {
        private readonly IDatabaseFactory _databaseFactory;

        public LookupDataValueToKeyDatabaseResolverFructose(IDatabaseFactory databaseFactory, ICacheFactory cacheFactory)
            : base(cacheFactory)
        {
            _databaseFactory = databaseFactory;
        }

        protected override Dictionary<T, string> GetDictionaryToCache(LookupDataByValue lookupDataByValue)
        {
            DataTable dataTable = _databaseFactory.Create(lookupDataByValue.DataSource)
                .Execute($"SELECT * FROM {lookupDataByValue.TableName}");

            string columnNameOfPrimaryKey = lookupDataByValue.ColumnNameOfPrimaryKey ?? dataTable.Columns[0].ColumnName;
            string columnNameOfFieldName = lookupDataByValue.ColumnNameOfFieldName ?? dataTable.Columns[1].ColumnName;

            return dataTable.AsEnumerable()
                .ToDictionary(dr => (T)dr[columnNameOfPrimaryKey], dr => dr[columnNameOfFieldName] == DBNull.Value ? null : dr[columnNameOfFieldName].ToString());
        }
    }

    public abstract class LookupDataResolverValueToKeyBaseFructose<T> : LookupDataResolverBaseFructose<LookupDataByValue, T>
    {
        private readonly ICache _cache;

        protected LookupDataResolverValueToKeyBaseFructose(ICacheFactory cacheFactory)
        {
            _cache = cacheFactory.Create();
        }

        protected abstract Dictionary<T, string> GetDictionaryToCache(LookupDataByValue lookupDataByValue);

        /// <summary>
        /// This is the protected AutoMapper method called within a mapper class when using opt.ResolveUsing()
        /// In this class, GetLookupValue() is called twice as a type of self-healing mechanism whereby if we can't find the value the first time, we'll clear the cache and try again
        /// This is because it's possible for a new value to be inserted into a lookup table after this cache is loaded but before the cache timeout expires
        /// If we don't find what we're looking for, we make a one-time assumption that the cache could be out of sync, so we refresh the cache and try one more time to get the expected value
        /// </summary>
        public override T Resolve(object source, object destination, LookupDataByValue sourceMember, T destMember, ResolutionContext context)
        {
            if (sourceMember == null || String.IsNullOrEmpty(sourceMember.Value))
            {
                return default(T);
            }

            string cacheKey = GetCacheKey(sourceMember.TableName);

            T result = GetLookupValue(sourceMember, cacheKey);

            if (EqualityComparer<T>.Default.Equals(result, default(T)))
            {
                _cache.Remove(cacheKey);

                result = GetLookupValue(sourceMember, cacheKey);
            }

            return result;
        }

        /// <summary>
        /// Exposing the AutoMapper trigger method allows clients to use this resolver outside of an AutoMapper as well
        /// </summary>
        public T Resolve(LookupDataByValue lookupDataByValue)
        {
            return Resolve(null, null, lookupDataByValue, default(T), null);
        }

        #region Private

        private T GetLookupValue(LookupDataByValue lookupDataByValue, string cacheKey)
        {
            Dictionary<T, string> lookupValues =
                _cache.GetOrAdd(cacheKey, () => GetDictionaryToCache(lookupDataByValue), GetCacheTimeoutInHours(lookupDataByValue));

            KeyValuePair<T, string> keyValuePair = lookupValues
                .SingleOrDefault(kvp => String.Equals(kvp.Value, lookupDataByValue.Value, StringComparison.OrdinalIgnoreCase));

            if (keyValuePair.Equals(default(KeyValuePair<int, string>)))
            {
                return default(T);
            }

            return keyValuePair.Key;
        }

        #endregion
    }

    public abstract class LookupDataResolverBaseFructose<TSource, TDestination> : IMemberValueResolver<object, object, TSource, TDestination>
    {
        private const int DefaultCacheTimeoutInHours = 24;
        private const string CacheKeyPrefix = "Reserved::Core::LookupData::";

        protected int GetCacheTimeoutInHours(LookupDataResolverBase lookupDataBase)
        {
            return lookupDataBase.CacheTimeoutInHours > 0 ? lookupDataBase.CacheTimeoutInHours : DefaultCacheTimeoutInHours;
        }

        protected string GetCacheKey(string tableName)
        {
            return $"{CacheKeyPrefix}{tableName}";
        }

        public abstract TDestination Resolve(object source, object destination, TSource sourceMember, TDestination destMember, ResolutionContext context);
    }
}
