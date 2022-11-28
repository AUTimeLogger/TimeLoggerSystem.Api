using AUTimeManagement.Api.Management.Api.Configuration;
using AUTimeManagement.Api.Management.Api.Security.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AUTimeManagement.Api.Management.Api.Security.DAL;

public class SecurityDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IOptions<DbConfigurationOptions> options;

    public SecurityDbContext(IOptions<DbConfigurationOptions> options)
    {
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var db = options.Value.Database;
        var connStr = options.Value.ConnectionString;

        if (string.IsNullOrEmpty(connStr))
        {
            throw new ArgumentNullException("ConnectionString");
        }

        switch (db)
        {
            case DbConfigurationOptions.DatabaseType.InMemory:
                optionsBuilder.UseInMemoryDatabase(connStr);
                break;
            case DbConfigurationOptions.DatabaseType.Sql:
                optionsBuilder.UseSqlServer(connStr, o =>
                    o.MigrationsAssembly(GetType().Assembly.FullName));
                break;
            case DbConfigurationOptions.DatabaseType.Postgres:
                optionsBuilder.UseNpgsql(connStr, o =>
                    o.MigrationsAssembly(GetType().Assembly.FullName));
                break;
            default: throw new InvalidOperationException();
        }

    }
}

