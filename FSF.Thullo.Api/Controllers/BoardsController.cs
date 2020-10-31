using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;

namespace FSF.Thullo.Api.Controllers
{
  [Route("api/Boards")]
  [ApiController]
  public class BoardsController : ControllerBase
  {
    private readonly ThulloService _thulloService;

    public BoardsController(ThulloService thulloService)
    {
      _thulloService = thulloService;
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_thulloService.GetBoards());
    }

    [HttpGet]
    [Route("{boardId}")]
    public IActionResult Get(int id)
    {
      return Ok(_thulloService.GetBoard(id));
    }

    [HttpPost]
    public IActionResult Post(Board board)
    {
      return Created(string.Empty, _thulloService.CreateBoard(board));
    }

    [HttpPut]
    [Route("{boardId}")]
    public IActionResult Put(int id, Board board)
    {
      return Ok(_thulloService.UpdateBoard(id, board));
    }

    [HttpDelete]
    [Route("{boardId}")]
    public IActionResult Delete(int id)
    {
      _thulloService.DeleteBoard(id);
      return Ok();
    }

   
  }
}