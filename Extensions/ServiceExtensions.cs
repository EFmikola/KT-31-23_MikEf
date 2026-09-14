using ProjectPractice.Services;

namespace ProjectPractice.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            services.AddScoped<IGroupService, GroupService>();

            return services;
        }
    }
}
