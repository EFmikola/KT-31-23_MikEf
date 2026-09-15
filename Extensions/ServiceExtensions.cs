using ProjectPractice.Services;

namespace ProjectPractice.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            services.AddScoped<IGroupService, GroupService>();

            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<IDisciplineService, DisciplineService>();

            services.AddScoped<IGradeService, GradeService>();
            return services;
        }
    }
}
