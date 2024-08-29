using SiGeP.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SiGeP.UI
{
    public static class ServiceExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            //Mucho muy importante
            services.AddScoped<AuthenticationService>();

            services.AddTransient<Syncfusion.Blazor.Spinner.SfSpinner>();

            services.AddScoped<ProvinceService>();

            //services.AddScoped<CustomerService>();

            //services.AddScoped<GenderService>();

        }
    }
}
