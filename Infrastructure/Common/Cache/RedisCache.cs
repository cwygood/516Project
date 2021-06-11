using Domain.Interfaces;
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

        public void Dispose()
        {
            
        }
    }
}
