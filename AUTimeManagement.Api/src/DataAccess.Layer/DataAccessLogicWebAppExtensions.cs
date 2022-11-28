using AUTimeManagement.Api.DataAccess.Layer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AUTimeManagement.Api.DataAccess.Layer;

public static class DataAccessLogicWebAppExtensions
{
    public static IApplicationBuilder UseBusinessDb(this IApplicationBuilder app, bool isDevelopment)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        bool relational = context.Database.IsRelational();

        if (isDevelopment && !relational)
        {
            context.Database.EnsureDeleted();
        }
        if (!relational)
        {
            context.Database.EnsureCreated();
        }
        else
        {
            context.Database.Migrate();
        }

        return app;
    }
}
