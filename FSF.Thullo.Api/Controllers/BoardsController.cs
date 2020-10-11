using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSF.Thullo.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BoardsController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(new string[] { "Board_1", "Board_2" });
    }
  }
}