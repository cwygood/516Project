using Domain.Interfaces;
using Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjectionExtension
{
    public static IServiceCollection AddEFCoreDbContext(this IServiceCollection services, IConfigurationSection section)
    {
        services.AddDbContext<EFCoreDbContext>(options =>
        {
            var connectionString = section.GetSection("ConnectionString").Value;
            options.UseMySQL(connectionString);
        });
        services.AddScoped<IDbHelper, EFCoreDbHelper>();
        return services;
    }
}
