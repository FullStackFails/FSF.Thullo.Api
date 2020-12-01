using FSF.Thullo.Core.Interfaces.DataAccess;
using FSF.Thullo.Core.Services;
using FSF.Thullo.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FSF.Thullo.Api
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(
            builder =>
            {
              builder.WithOrigins("http://localhost:3000");
            });
      });
      services.AddControllers();
      services.AddHealthChecks();

      RegisterCustomServices(services);
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

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHealthChecks("/healthcheck");
        endpoints.MapControllers();
      });
    }

    private void RegisterCustomServices(IServiceCollection services)
    {
      services.AddScoped<ThulloService>();
      services.AddScoped<IThulloRepository, ThulloRepository>();
    }
  }
}
