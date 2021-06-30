using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SalesForce.Api.Configuration
{
    public static class HealthCheckConfig
    {
        public static void HealthCheckConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(Configuration.GetConnectionString("DefaultConnection"), name: "SQLServer")
                .AddRedis(Configuration.GetConnectionString("DefaultConnectionCache"), name: "Redis");

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(15);
                opt.MaximumHistoryEntriesPerEndpoint(60);
                opt.SetApiMaxActiveRequests(1);

                opt.AddHealthCheckEndpoint("default api", "/healthz");
            })
            .AddInMemoryStorage();
        }

        public static void HealthCheckConfigure(this IApplicationBuilder app, IConfiguration Configuration)
        {
            app.UseHealthChecks("/api/hc");

            app.UseEndpoints(endpoints =>
            {
                //adding endpoint of health check for the health check ui in UI format
                endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                //map healthcheck ui endpoing - default is /healthchecks-ui/
                endpoints.MapHealthChecksUI();

                endpoints.MapGet("/", async context => await context.Response.WriteAsync("Hello World!"));
            });
        }
    }
}
