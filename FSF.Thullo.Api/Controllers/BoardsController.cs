using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSF.Thullo.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BoardsController : ControllerBase
  {
    private readonly BoardService _boardService;

    public BoardsController(BoardService boardService)
    {
      _boardService = boardService;
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_boardService.Get());
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Get(int id)
    {
      return Ok(_boardService.Get(id));
    }

    [HttpPost]
    public IActionResult Post(Board board)
    {
      _boardService.Create(board);
      return Created(string.Empty, null);
    }

    [HttpPut]
    public IActionResult Put(Board board)
    {
      _boardService.Update(board);
      return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
      _boardService.Delete(id);
      return Ok();
    }
  }
}