using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SalesForce.Api.Configuration
{
    public static class CacheConfig
    {
        public static void CacheConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration =
                    Configuration.GetConnectionString("DefaultConnectionCache");
                options.InstanceName = "SalesForce";
            });
        }
    }
}
