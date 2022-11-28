using AUTimeManagement.Api.Business.Logic;
using AUTimeManagement.Api.Management.Api.Configuration;
using AUTimeManagement.Api.Management.Api.Security.DAL;
using AUTimeManagement.Api.Management.Api.Security.Model;
using AUTimeManagement.Api.Management.Api.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        services.AddAutoMapper(typeof(ApiServiceCollectionExtensions));

        services.AddTransient<ITokenGenerator, DefaultTokenGenerator>();

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    private static void AddCustomSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbConfigurationOptions>(configuration.GetSection(DbConfigurationOptions.SectionName));

        var jwt = configuration.GetRequiredSection(TokenGenerationOption.SectionName);

        services.Configure<TokenGenerationOption>(jwt);

        string issuer = jwt.GetValue<string>("ValidIssuer");
        string audience = jwt.GetValue<string>("ValidAudience");
        string key = jwt.GetValue<string>("SigningSecret");

        services.AddDbContext<SecurityDbContext>();
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<SecurityDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });

        services.AddAuthorization();
    }
}