using AUTimeManagement.Api.DataAccess.Layer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AUTimeManagement.Api.DataAccess.Layer;

public static class DataAccessLogicWebAppExtensions
{
    public static IApplicationBuilder UseBusinessDb(this IApplicationBuilder app, bool isDevelopment)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        if (isDevelopment)
        {
            context.Database.EnsureDeleted();
        }

        context.Database.EnsureCreated();

        return app;
    }
}
