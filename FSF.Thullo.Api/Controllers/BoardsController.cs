using FSF.Thullo.Core.Dto.BoardDtos;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace FSF.Thullo.Api.Controllers
{
  /// <summary>
  /// This controller provides an interface for working with boards.
  /// Boards are the root/parent resource to lists and cards.
  /// </summary>
  [Route("api/Boards")]
  [ApiController]
  public class BoardsController : ControllerBase
  {
    private readonly ThulloService _thulloService;

    /// <summary>
    /// Constructor for the boards controller.
    /// </summary>
    /// <param name="thulloService">Delegation service for Thullo business logic.</param>
    public BoardsController(ThulloService thulloService)
    {
      _thulloService = thulloService;
    }

    /// <summary>
    /// Gets all boards.
    /// </summary>
    /// <returns>A collection of boards.</returns>
    [HttpGet]
    public IActionResult Get()
    {
      var boards = _thulloService.GetBoards().Select(b => BoardDto.FromBoard(b));
      return Ok(boards);
    }

    /// <summary>
    /// Gets a single board.
    /// </summary>
    /// <param name="boardId">The list of the board.</param>
    /// <returns>A single board.</returns>
    [HttpGet]
    [Route("{boardId}")]
    public IActionResult Get(int boardId)
    {
      var board = BoardDto.FromBoard(_thulloService.GetBoard(boardId));
      return Ok(board);
    }

    /// <summary>
    /// Creates a new board.
    /// </summary>
    /// <param name="dto">The representation of the new board to be created.</param>
    /// <returns>The newly created board.</returns>
    [HttpPost]
    public IActionResult Post(BoardForCreationDto dto)
    {
      Board board = BoardForCreationDto.ToBoard(dto);

      var createdBoard = BoardDto.FromBoard(_thulloService.CreateBoard(board));
      return Created(string.Empty, createdBoard);
    }

    /// <summary>
    /// Updates an existing board.
    /// </summary>
    /// <param name="boardId">The id of the board.</param>
    /// <param name="dto">The updated representation of the board.</param>
    /// <returns>The updated representation of the board.</returns>
    [HttpPut]
    [Route("{boardId}")]
    public IActionResult Put(int boardId, BoardForUpdateDto dto)
    {
      Board board = BoardForUpdateDto.ToBoard(dto);

      var updatedBoard = BoardDto.FromBoard(_thulloService.UpdateBoard(boardId, board));
      return Ok(updatedBoard);
    }

    /// <summary>
    /// Deletes a board.
    /// </summary>
    /// <param name="boardId">The id of the board.</param>
    [HttpDelete]
    [Route("{boardId}")]
    public IActionResult Delete(int boardId)
    {
      _thulloService.DeleteBoard(boardId);
      return Ok();
    }

   
  }
}