using Domain.Interfaces;
using Infrastructure.Common.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

public static partial class DependencyInjectionExtension
{
    public static IServiceCollection AddCache(this IServiceCollection services, IConfigurationSection section)
    {
        var host = section.GetSection("Host").Value;
        var port = section.GetSection("Port").Value;
        var pwd = section.GetSection("Password").Value;
        var defaultDb = section.GetSection("DbIndex").Value;

        services.AddSingleton<IRedisCache>(sp => new RedisCache($"{host}:{port},defaultDatabase={defaultDb},password={pwd}", Convert.ToInt32(defaultDb)));
        services.AddMemoryCache();
        return services;
    }
}