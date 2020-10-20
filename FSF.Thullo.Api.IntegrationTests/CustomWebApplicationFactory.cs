using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FSF.Thullo.Api.IntegrationTests
{
  /// <summary>
  /// This class is used to help with setup of Microsoft's
  /// Mvc Testing class 'WebApplicationFactory'..
  /// </summary>
  /// <see cref="https://github.com/willj/aspnet-core-mstest-integration-sample"/>
  public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
  {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      base.ConfigureWebHost(builder);
    }
  }
}
