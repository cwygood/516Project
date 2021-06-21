﻿using Domain.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common.Cache
{
    public class RedisCache : IRedisCache
    {
        //todo : redis集成
        private readonly string _connectString;
        private readonly int _defaultDb;
        public RedisCache(string connectionString, int defaultDb = 0)
        {
            this._connectString = connectionString;
            this._defaultDb = defaultDb;
        }

        private ConnectionMultiplexer GetConnection()
        {
            return ConnectionMultiplexer.Connect(this._connectString);
        }
        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <returns></returns>
        public IDatabase GetDatabase()
        {
            return this.GetConnection().GetDatabase(this._defaultDb);
        }

        public IServer GetServer()
        {
            return this.GetConnection().GetServer(this._connectString);
        }

        public ISubscriber GetSubscriber()
        {
            return this.GetConnection().GetSubscriber();
        }
        public bool SetString(string key, string value, TimeSpan? span = null, string prefix = "")
        {
            var curKey = key;
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                curKey = prefix + ":" + key;
            }
            return this.GetDatabase().StringSet(curKey, value, span);
        }
        public string GetString(string key, string prefix = "")
        {
            var curKey = key;
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                curKey = prefix + ":" + key;
            }
            return this.GetDatabase().StringGet(curKey);
        }

        public void Dispose()
        {
            
        }
    }
}
