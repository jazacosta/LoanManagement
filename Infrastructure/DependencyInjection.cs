//using Core.Interfaces.Repositories;
//using Core.Interfaces.Services;
using Infrastructure.Contexts;
//using Infrastructure.Repositories;
//using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            //services.AddServices();
            //services.AddRepositories();

            return services;
        }

        //public static IServiceCollection AddRepositories(this IServiceCollection services)
        //{
        //    services.AddScoped<ILoanRepository, LoanRepository>();

        //    return services;
        //}

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LoanManagement");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;

        }

        //public static IServiceCollection AddServices(this IServiceCollection services)
        //{
        //    services.AddScoped<ILoanService, LoanService>();

        //    return services;
        //}
    }
}