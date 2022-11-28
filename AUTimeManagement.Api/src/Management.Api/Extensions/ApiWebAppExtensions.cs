using AUTimeManagement.Api.Business.Logic;
using AUTimeManagement.Api.Management.Api.Security.DAL;
using AUTimeManagement.Api.Management.Api.Security.Model;
using Microsoft.EntityFrameworkCore;

namespace AUTimeManagement.Api.Management.Api.Extensions;

public static class ApiWebAppExtensions
{
    public static async Task<WebApplication> UseDbSeedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<SecurityDbContext>();
        using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        using var roleManage = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        bool isDevelopment = app.Environment.IsDevelopment();
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
            await context.Database.MigrateAsync();
        }

        if (!roleManage.Roles.Any())
        {
            await roleManage.CreateAsync(new IdentityRole("Admin"));
            await roleManage.CreateAsync(new IdentityRole("User"));
        }

        if (isDevelopment)
        {
            string? password = app.Configuration["SecurityDbConfig:rootPassword"];

            if (await userManager.FindByEmailAsync("root@root.com") is null)
            {
                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException(nameof(password));
                }

                ApplicationUser user = new ApplicationUser { Email = "root@root.com", EmailConfirmed = true, UserName = "root", };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    Console.WriteLine("Root added!");
                }
            }
            Console.WriteLine("Root already exists!");
        }
        else
        {
            bool isEmpty = context.Users.Any();
            if (isEmpty)
            {
                string? password = app.Configuration["SecurityDbConfig:rootPassword"];
                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException(nameof(password));
                }
                var user = new ApplicationUser
                {
                    Email = "root@root.com",
                    EmailConfirmed = true,
                    UserName = "root",
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    Console.WriteLine("Root added!");
                }
            }
        }
        var root = await userManager.FindByNameAsync("root");
        if (root != null)
        {
            await userManager.AddToRoleAsync(root, "Admin");
        }

        app.UseBusinessLogic(isDevelopment);

        return app;
    }
}
