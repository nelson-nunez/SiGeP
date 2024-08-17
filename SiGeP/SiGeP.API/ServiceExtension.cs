using SiGeP.Business;
using SiGeP.DataAccess.Generic;
using SiGeP.DataAccess.Repositories;

namespace SiGeP.API
{
    public static class ServiceExtension
    {
        public static void AddInfraestructureServices(this IServiceCollection services)
        {

        }

        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<UnitOfWork, UnitOfWork>();

            services.AddScoped<CustomerRepository, CustomerRepository>();

            services.AddScoped<GenderRepository, GenderRepository>();

            services.AddScoped<AppUserRepository, AppUserRepository>();
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<CustomerBusiness, CustomerBusiness>();

            services.AddScoped<GenderBusiness, GenderBusiness>();

            services.AddScoped<AuthenticationBusiness, AuthenticationBusiness>();
        }
    }
}