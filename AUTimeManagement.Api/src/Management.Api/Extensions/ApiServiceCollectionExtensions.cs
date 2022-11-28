using AUTimeManagement.Api.Business.Logic;
using AUTimeManagement.Api.Management.Api.Security.DAL;
using AUTimeManagement.Api.Management.Api.Security.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AUTimeManagement.Api.Management.Api.Extensions;

public static class ApiServiceCollectionExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        //TODO add honeycomb

        builder.Services.ConfigureServices(builder.Configuration);
        return builder;
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCustomSecurity(configuration);

        services.AddBusinessLogic(configuration);



        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    private static void AddCustomSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO add configuration...

        services.AddDbContext<SecurityDbContext>(options => options.UseInMemoryDatabase("SecDb"));
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<SecurityDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options=>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });

        services.AddAuthorization();
    }
}