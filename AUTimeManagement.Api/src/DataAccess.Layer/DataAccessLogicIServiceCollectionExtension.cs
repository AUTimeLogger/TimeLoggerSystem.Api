using Microsoft.Extensions.DependencyInjection;

namespace AUTimeManagement.Api.DataAccess.Layer;

public static class DataAccessLogicIServiceCollectionExtension
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        return services;
    }
}

