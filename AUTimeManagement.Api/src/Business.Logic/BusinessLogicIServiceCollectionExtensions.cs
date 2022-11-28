using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AUTimeManagement.Api.DataAccess.Layer;
using Microsoft.Extensions.Configuration;

namespace AUTimeManagement.Api.Business.Logic;

public static class BusinessLogicIServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccessLayer(options =>
        {
            options.Database = DataAccessOptions.DatabaseType.InMemory;
            options.ConnectionString = configuration["Business:ConnectionString"];
        });

        return services;
    }

    public static IApplicationBuilder UseBusinessLogic(this IApplicationBuilder app, bool isDevelopment)
    {
        app.UseBusinessDb(isDevelopment);

        return app;
    }
}

