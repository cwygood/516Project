using Infrastructure.Configurations;
using Infrastructure.Jaeger;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

public static partial class DependencyInjectionExtension
{
    public static IServiceCollection AddJaeger(this IServiceCollection services, IConfigurationSection section)
    {
        services.Configure<JaegerConfiguration>(section);
        var cfg = section.Get<JaegerConfiguration>();
        var host = string.IsNullOrWhiteSpace(cfg.Host) ? UdpSender.DefaultAgentUdpHost : cfg.Host;
        var port = cfg.Port <= 100 ? UdpSender.DefaultAgentUdpCompactPort : cfg.Port;
        var maxPacketSize = cfg.MaxPacketSize < 0 ? 0 : cfg.MaxPacketSize;
        services.AddOpenTracing();
        services.AddSingleton<ITracer>(provider =>
        {
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            var reporter = new RemoteReporter.Builder().WithLoggerFactory(loggerFactory)
                                                     //.WithSender(new UdpSender("localhost", 6831, 0))
                                                     .WithSender(new HttpSender("http://localhost:14268/api/traces"))
                                                     .Build();
            var serviceName = Assembly.GetEntryAssembly().GetName().Name;
            ITracer tracer = new Tracer.Builder(DateTime.Now.ToString("yyyyMMddHHmmssfff"))
                                     .WithReporter(reporter)
                                     .WithLoggerFactory(loggerFactory)
                                     .WithSampler(new ConstSampler(true))
                                     .Build();

            //var serviceName = provider.GetRequiredService<IWebHostEnvironment>().ApplicationName;
            //var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

            //Jaeger.Configuration.SenderConfiguration.DefaultSenderResolver = new SenderResolver(loggerFactory)
            //    .RegisterSenderFactory<ThriftSenderFactory>();

            //var tracer = new Tracer.Builder(serviceName)
            //    .WithLoggerFactory(loggerFactory)
            //    .WithSampler(new ConstSampler(true))
            //    .Build();

            //GlobalTracer.Register(tracer);

            return tracer;
        });
        return services;
    }

    public static IApplicationBuilder UseJaeger(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JaegerMiddleware>();
    }
}
