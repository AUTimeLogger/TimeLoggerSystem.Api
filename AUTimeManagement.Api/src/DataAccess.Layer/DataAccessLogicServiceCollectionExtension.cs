using AUTimeManagement.Api.DataAccess.Layer.Context;
using Microsoft.Extensions.DependencyInjection;

namespace AUTimeManagement.Api.DataAccess.Layer;

public static class DataAccessLogicServiceCollectionExtension
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        //services.AddDbContextPool<DataContext>(o => o.UseMemoryCache(null), poolSize: 8);
        services.AddDbContext<DataContext>();
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

