using FSF.Thullo.Core.Interfaces.DataAccess;
using FSF.Thullo.Core.Services;
using FSF.Thullo.Infrastructure.DataAccess;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace FSF.Thullo.Api
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddHealthChecks();

      RegisterCustomServices(services);

      services.AddCors(options =>
      {
        options.AddDefaultPolicy(
            builder =>
            {
              builder.WithOrigins(new string[] { "http://localhost:3000", "https://localhost:44329" })
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
            });
      });
      
      // Add Bearer token authentication
      services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
              options.Authority = "https://localhost:5001";

              options.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateAudience = false
              };
            });

      services.AddAuthorization(options =>
      {
        options.AddPolicy("ApiScope", policy =>
        {
          policy.RequireAuthenticatedUser();
          policy.RequireClaim("scope", "thulloApi");
        });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseCors();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHealthChecks("/healthcheck");
        endpoints.MapControllers()
        .RequireAuthorization("ApiScope");
      });
    }

    private void RegisterCustomServices(IServiceCollection services)
    {
      services.AddScoped<ThulloService>();
      services.AddScoped<IThulloRepository, ThulloRepository>();
    }
  }
}
