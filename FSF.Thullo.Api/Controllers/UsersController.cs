using FSF.Thullo.Core.Dto;
using FSF.Thullo.Core.Interfaces.Security;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FSF.Thullo.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class UsersController : ControllerBase
  {
    private readonly UserService _userService;
    private readonly ISessionService _sessionService;

    public UsersController(UserService userService, ISessionService sessionService)
    {
      _userService = userService;
      _sessionService = sessionService;
    }

    [HttpGet]
    [Route("{userId}")]
    public ActionResult<UserDto> Get(Guid userId)
    {
      // ISession session = _sessionService.GetSession(User);

      var user = UserDto.FromUser(_userService.GetUserById(userId));

      return Ok(user);
    }

    [HttpPost]
    [Route("")]
    public ActionResult<List<UserDto>> Get(List<Guid> userIds)
    {
      // ISession session = _sessionService.GetSession(User);
      var users = UserDto.FromUsers(_userService.GetUsersByIds(userIds));

      return Ok(users);
    }
  }
}
