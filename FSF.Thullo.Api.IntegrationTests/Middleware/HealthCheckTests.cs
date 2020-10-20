using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace FSF.Thullo.Api.IntegrationTests.Middleware
{
  [TestClass]
  public class HealthCheckTests
  {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public HealthCheckTests()
    {
      _factory = new CustomWebApplicationFactory<Startup>();
      _client = _factory.CreateDefaultClient();
    }

    [TestMethod]
    public async Task HealthCheck_ReturnsOk()
    {
      var response = await _client.GetAsync("/healthcheck");

      response.EnsureSuccessStatusCode();
    }
  }
}
