using FSF.Thullo.Core.Dto.BoardDtos;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace FSF.Thullo.Api.Controllers
{
  [Route("api/Boards")]
  [ApiController]
  [Authorize]
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
      var boards = _thulloService.GetBoards().Select(b => BoardDto.FromBoard(b));
      return Ok(boards);
    }

    [HttpGet]
    [Route("{boardId}")]
    public IActionResult Get(int boardId)
    {
      var board = BoardDto.FromBoard(_thulloService.GetBoard(boardId));
      return Ok(board);
    }

    [HttpPost]
    public IActionResult Post(BoardForCreationDto dto)
    {
      Board board = BoardForCreationDto.ToBoard(dto);

      var createdBoard = BoardDto.FromBoard(_thulloService.CreateBoard(board));
      return Created(string.Empty, createdBoard);
    }

    [HttpPut]
    [Route("{boardId}")]
    public IActionResult Put(int boardId, BoardForUpdateDto dto)
    {
      Board board = BoardForUpdateDto.ToBoard(dto);

      var updatedBoard = BoardDto.FromBoard(_thulloService.UpdateBoard(boardId, board));
      return Ok(updatedBoard);
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