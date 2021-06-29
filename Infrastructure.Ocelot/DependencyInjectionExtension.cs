using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System;
using System.Collections.Generic;
using System.Text;

public static partial class DependencyInjectionExtension
{
    public static IServiceCollection AddMyOcelot(this IServiceCollection services)
    {
        services.AddOcelot()
                .AddConsul()
                .AddCacheManager(setting =>
                {
                    setting.WithDictionaryHandle();
                });
        return services;
    }

    public static IApplicationBuilder UseMyOcelot(this IApplicationBuilder app)
    {
        app.UseOcelot().Wait();//报错FluentValidation某个方法不存在，此时需要将FluentValidation组件回退到ocelot引用的版本9.3.0
        return app;
    }
}
