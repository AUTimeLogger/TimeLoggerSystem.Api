using AUTimeManagement.Api.DataAccess.Layer.Context;
using AUTimeManagement.Api.DataAccess.Layer.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace AUTimeManagement.Api.DataAccess.Layer;

public static class DataAccessLogicServiceCollectionExtension
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        /*services.TryAddEnumerable(
           ServiceDescriptor.Transient<IConfigureOptions<DataAccessOptions>, DataAccessOptionsSetup>());*/

        services.Configure<DataAccessOptions>(x => { 
            x.ConnectionString = "TestDb"; 
            x.Database = DataAccessOptions.DatabaseType.InMemory; 
        });

        //services.AddDbContextPool<DataContext>(o => o.UseMemoryCache(null), poolSize: 8);
        services.AddDbContext<DataContext>();

        services.TryAddTransient<IDataReader, DefaultDataReader>();
        services.TryAddScoped<IDataWriter, DefaultDataWriter>();

        return services;
    }

    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, Action<DataAccessOptions> setupAction)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (setupAction is null)
        {
            throw new ArgumentNullException(nameof(setupAction));
        }

        services.AddDataAccessLayer();

        services.Configure(setupAction);

        return services;
    }
}

public sealed class DataAccessOptionsSetup : IConfigureOptions<DataAccessOptions>
{
    public void Configure(DataAccessOptions options)
    {
        if(options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        options.ConnectionString = "TestDb";
        options.Database = DataAccessOptions.DatabaseType.InMemory;
    }
}