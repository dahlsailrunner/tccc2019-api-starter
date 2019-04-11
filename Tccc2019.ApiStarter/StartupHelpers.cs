using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Tccc2019.ApiStarter
{
    public static class StartupHelpers
    {
        public static IServiceCollection AddSwaggerGenWithIdentityServer(this IServiceCollection services, 
            IConfiguration config, string apiName)
        {
            var apiScope = config.GetValue<string>("ApiName");
            var scopes = apiScope.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var oauthScopeDic = new Dictionary<string, string>();
            foreach (var scope in scopes)
            {
                oauthScopeDic.Add(scope, $"Access to the {scope} API");
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = apiName, Version = "v1" });
                var authority = config.GetValue<string>("Authority");
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{authority}/connect/authorize",
                    Scopes = oauthScopeDic
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", scopes }
                });
            });
            return services;
        }
    }
}
