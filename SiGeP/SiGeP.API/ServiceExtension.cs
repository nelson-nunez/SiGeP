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

            services.AddScoped<AppUserRepository, AppUserRepository>();
            //Person
            services.AddScoped<GenderRepository, GenderRepository>();
            
            services.AddScoped<CustomerRepository, CustomerRepository>();
          
            services.AddScoped<DoctorRepository, DoctorRepository>();           
            //Address
            services.AddScoped<CityRepository, CityRepository>();           
            services.AddScoped<NeighborhoodRepository, NeighborhoodRepository>();           
            services.AddScoped<ProvinceRepository, ProvinceRepository>();           
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationBusiness, AuthenticationBusiness>();
            
            //Person
            services.AddScoped<CustomerBusiness, CustomerBusiness>();

            services.AddScoped<GenderBusiness, GenderBusiness>();
                        
            services.AddScoped<DoctorBusiness, DoctorBusiness>();

            //Address
            services.AddScoped<CityBusiness, CityBusiness>();

            services.AddScoped<ProvinceBusiness, ProvinceBusiness>();

            services.AddScoped<NeighborhoodBusiness, NeighborhoodBusiness>();

        }
    }
}