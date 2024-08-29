using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using SiGeP.API.Common.Model;
using SiGeP.API.Common;
using Syncfusion.Blazor;
using System.Globalization;
using System.Net.Http.Headers;
using SiGeP.UI.Data;
using SiGeP.UI;

var builder = WebApplication.CreateBuilder(args);



#region Auth

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    //options.Cookie.Name = "AUTH_COOKIE";
    //options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.SlidingExpiration = true;
    options.LoginPath = "/LoginPage";
    options.AccessDeniedPath = "/AccessDenied";
});

#endregion

builder.Services.AddHttpClient<WebApiClient>("BaseApiConfig", client =>
{
    var webApiConfig = AppConfiguration.GetConfigurationSection<WebApiConfig>("BaseApiConfig");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/x-json"));
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/javascript"));
    client.BaseAddress = new Uri(webApiConfig.BaseAddress);
    client.Timeout = TimeSpan.FromMinutes(webApiConfig.Timeout);
});

#region Services Razor Pages, Blazor y Syncfusion

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>()
    {
        new CultureInfo("en-US"),
        new CultureInfo("es-ES")
    };
    // Configurar la cultura predeterminada 
    var culture = new CultureInfo("es-ES");
    culture.NumberFormat.NumberDecimalSeparator = ".";
    culture.NumberFormat.NumberGroupSeparator = ",";
    options.DefaultRequestCulture = new RequestCulture(culture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>() {
     new QueryStringRequestCultureProvider() // Puedes usar otros proveedores de localizaci�n aqu�
    };
});

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQ5MTY3N0AzMTM5MmUzMjJlMzBoQzFJT1BOTnNSbkpkT2lZd2w1RFBJeGxrYkdvUFVBMFp1bzhpQkpYaWU0PQ==");

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));

#endregion

#region Services Infrastructure

builder.Services.AddLocalization();

builder.Services.AddControllers();

builder.Services.AddInfrastructureServices();

#endregion

#region Old Configure Section

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization(new RequestLocalizationOptions().AddSupportedCultures(new[] { "en-US", "en-US" }).AddSupportedUICultures(new[] { "en-US", "en-US" }));
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

#endregion
