using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSF.Thullo.Api.Controllers
{
  /// <summary>
  /// Demo Controller for setting up OpenIdConnect.
  /// TODO: Remove this controller once App is connected via bearer tokens.
  /// </summary>
  [Route("api/identity")]
  [ApiController]
  [Authorize]
  public class IdentityController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      var request = HttpContext.Request;

      return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
    }
  }
}
