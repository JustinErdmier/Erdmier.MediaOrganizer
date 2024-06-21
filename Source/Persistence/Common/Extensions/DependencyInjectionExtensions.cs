using Erdmier.MediaOrganizer.Persistence.Contexts;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Erdmier.MediaOrganizer.Persistence.Common.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddApplicationDbContext(configuration, environment);

        return services;
    }

    private static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        string connectionString = configuration[ApplicationDbContext.ConnectionStringKey] ??
                                  throw new InvalidOperationException(message: $"Connection string '{ApplicationDbContext.ConnectionStringKey}' not found.");

        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            if (environment.IsDevelopment())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }

            options.UseSqlServer(connectionString,
                                 sqlOptions =>
                                 {
                                     sqlOptions.MigrationsAssembly(assemblyName: "Persistence");

                                     sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                 });
        });

        return services;
    }
}
