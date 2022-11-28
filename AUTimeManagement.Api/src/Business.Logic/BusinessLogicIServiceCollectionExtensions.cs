using AUTimeManagement.Api.Business.Logic.Internal;
using AUTimeManagement.Api.Business.Logic.Services;
using AUTimeManagement.Api.DataAccess.Layer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AUTimeManagement.Api.Business.Logic;

public static class BusinessLogicIServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
    {
        DataAccessOptions cfg = new();
        cfg.Database = Enum.Parse<DataAccessOptions.DatabaseType>(configuration["Business:Database"]!);
        cfg.ConnectionString = configuration["Business:ConnectionString"];

        services.AddDataAccessLayer(options =>
        {
            options.Database = cfg.Database;
            options.ConnectionString = cfg.ConnectionString;
        });

        services.TryAddScoped<IBusinessService, DefaultBusinessService>();
        services.TryAddTransient<IProjectService, DefaultProjectService>();
        services.TryAddTransient<IReportService, DefaultReportService>();

        return services;
    }

    public static IApplicationBuilder UseBusinessLogic(this IApplicationBuilder app, bool isDevelopment)
    {
        app.UseBusinessDb(isDevelopment);

        return app;
    }
}

