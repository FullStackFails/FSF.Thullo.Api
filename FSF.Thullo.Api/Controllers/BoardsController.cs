using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

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
      Board newBoard = _boardService.Create(board);
      return Created(string.Empty, newBoard);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Put(int id, Board board)
    {
      _boardService.Update(id, board);
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