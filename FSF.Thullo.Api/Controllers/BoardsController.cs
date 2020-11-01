using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Cors;
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
    public IActionResult Get(int boardId)
    {
      return Ok(_thulloService.GetBoard(boardId));
    }

    [HttpPost]
    public IActionResult Post(Board board)
    {
      return Created(string.Empty, _thulloService.CreateBoard(board));
    }

    [HttpPut]
    [Route("{boardId}")]
    public IActionResult Put(int boardId, Board board)
    {
      return Ok(_thulloService.UpdateBoard(boardId, board));
    }

    [HttpDelete]
    [Route("{boardId}")]
    public IActionResult Delete(int boardId)
    {
      _thulloService.DeleteBoard(boardId);
      return Ok();
    }

   
  }
}