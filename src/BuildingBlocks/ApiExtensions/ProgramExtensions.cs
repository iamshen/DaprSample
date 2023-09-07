using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;
 
public static class ProgramExtensions
{
    
    public static void AddAppSwagger(this WebApplicationBuilder builder, string appName)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"HuangsOnDapr - {appName}", Version = "v1" });

            var identityUrlExternal = builder.Configuration.GetValue<string>("IdentityUrlExternal");

            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{identityUrlExternal}/connect/authorize"),
                        TokenUrl = new Uri($"{identityUrlExternal}/connect/token"),
                        Scopes = new Dictionary<string, string>()
                        {
                            { "daprTool", appName }
                        }
                    }
                }
            });

            c.OperationFilter<AuthorizeCheckOperationFilter>();
        });
    }

    public static void UseAppSwagger(this WebApplication app, string appName)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} V1");
            c.OAuthClientId("daprToolswaggerui");
            c.OAuthAppName("daprTool Swagger UI");
        });
    }
    
      
    public static void AddAppHealthChecks(this WebApplicationBuilder builder, string sqlCheckName, string connectionStringName = "default")
    {
        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddDapr()
            .AddNpgSql(
                builder.Configuration[$"ConnectionStrings:{connectionStringName}"]!,
                name: sqlCheckName ,
                tags: new[] { sqlCheckName });
        
    }

}