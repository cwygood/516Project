using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IRedisCache : IDisposable
    {
        IDatabase GetDatabase();
        IServer GetServer();
        ISubscriber GetSubscriber();
    }
}
