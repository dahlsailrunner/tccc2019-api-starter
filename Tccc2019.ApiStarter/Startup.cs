using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tccc2019.ApiBooster.Filters;
using Tccc2019.ApiBooster.Middleware;

namespace Tccc2019.ApiStarter
{
    public class Startup
    {
        // TODO: Review appsettings.json and set values appropriately for dev
        // TODO: Update ApiName constant to reflect your own API
        private const string ApiName = "TCCC 2019 API Starter";
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }        

        public void ConfigureServices(IServiceCollection services)
        {                                    
            var authority = Configuration.GetValue<string>("Authority");
            var apiName = Configuration.GetValue<string>("ApiName");
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authority;
                    options.ApiName = apiName;
                });

            services.AddAuthorization();
            services.AddCors();
            services.AddMvc(options =>
                {
                    var builder = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser();

                    options.Filters.Add(new AuthorizeFilter(builder.Build()));
                    options.Filters.Add(new TrackPerformanceFilter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGenWithIdentityServer(Configuration, ApiName);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseApiExceptionHandler(opts => { opts.AddResponseDetails = AddCustomErrorInfo; });

            var corsOrigins = Configuration.GetValue<string>("CorsOrigins");
            app.UseCors(builder => builder
                .WithOrigins(corsOrigins)
                .AllowAnyHeader()                   
                .AllowAnyMethod());

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName);
                options.OAuthClientId("implicit");  // should represent the swagger UI
            });

            app.UseAuthentication();
            app.UseMvc();
        }

        private void AddCustomErrorInfo(HttpContext ctx, Exception ex, ApiError error)
        {
            error.Links = Configuration.GetValue<string>("ApiErrorUrl");
        }
    }
}
