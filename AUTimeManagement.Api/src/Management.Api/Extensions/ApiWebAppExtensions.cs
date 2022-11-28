using AUTimeManagement.Api.Business.Logic;
using AUTimeManagement.Api.Management.Api.Security.DAL;
using AUTimeManagement.Api.Management.Api.Security.Model;

namespace AUTimeManagement.Api.Management.Api.Extensions;

public static class ApiWebAppExtensions
{
    public static async Task<WebApplication> UseDbSeedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<SecurityDbContext>();
        using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        bool isDevelopment = app.Environment.IsDevelopment();

        if (isDevelopment)
        {
            context.Database.EnsureDeleted();
        }

        context.Database.EnsureCreated();

        if (isDevelopment)
        {
            string password = app.Configuration["rootPassword"];
            if(await userManager.FindByEmailAsync("root@root.com") is not null)
            {
                ApplicationUser user = new ApplicationUser { Email = "root@root.com", EmailConfirmed = true, UserName = "root", };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    Console.WriteLine("Root added!");
                }
            }
            Console.WriteLine("Root already exists!");
        }

        app.UseBusinessLogic(isDevelopment);

        return app;
    }
}
