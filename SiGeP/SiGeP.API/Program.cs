using Microsoft.IdentityModel.Tokens;
using SiGeP.API.Common.Model;
using SiGeP.API.Common;
using SiGeP.API.LogConfiguration;
using SiGeP.API.Mapper;
using SiGeP.API;
using SiGeP.Model;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Serilog.Events;
using Serilog.Enrichers;
using SiGeP.DataAccess.Generic;
using SiGeP.DataAccess.Repositories;
using SiGeP.Business;

var builder = WebApplication.CreateBuilder(args);

#region Configuración de Serilog

//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//    .Build();

//var connectionStringSection = configuration.GetSection("ConnectionStrings:LogsConnection");

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Debug()
//    .Filter.ByExcluding((le) => le.Level == LogEventLevel.Information)
//    .Filter.ByExcluding((le) => le.Level == LogEventLevel.Debug)
//    .Enrich.WithProcessId()
//    .Enrich.WithProcessName()
//    .Enrich.FromLogContext()
//    .WriteTo.MSSqlServer(
//        connectionString: connectionStringSection.Value,
//        appConfiguration: configuration,
//        sinkOptions: SerilogConfiguration.GetsSinkOptions(),
//        columnOptions: SerilogConfiguration.GetColumnOptions())
//    .CreateLogger();

//// Configurar Serilog como proveedor de logs
//builder.Host.UseSerilog();

#endregion

#region Agregar servicios al contenedor
 
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

#endregion

#region SWAGGER

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SiGeP", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

#endregion

#region Security

var jwtTokenConfig = builder.Configuration.GetSection("JwtTokenConfig").Get<JwtTokenConfig>();
builder.Services.AddSingleton(jwtTokenConfig);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtTokenConfig.Issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
        ValidAudience = jwtTokenConfig.Audience,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1)
    };
});
builder.Services.AddSingleton<IJwtAuthManager, JwtAuthManager>();

#endregion

#region Servicios personalizados

builder.Services.AddDbContext<DbModelContext>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDataAccessServices();

builder.Services.AddBusinessServices();

builder.Services.AddInfraestructureServices();

var app = builder.Build();

#endregion

#region Configure

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SiGeP v1"));
}

app.UseHttpsRedirection();

//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
