using FSF.Thullo.Core.Dto.BoardDtos;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.Security;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;

namespace FSF.Thullo.Api.Controllers
{
  /// <summary>
  /// This controller provides an interface for working with boards.
  /// Boards are the root/parent resource to lists and cards.
  /// </summary>
  [Route("api/Boards")]
  [ApiController]
  [Authorize]
  public class BoardsController : ControllerBase
  {
    private readonly ThulloService _thulloService;
    private readonly ISessionService _sessionService;

    /// <summary>
    /// Constructor for the boards controller.
    /// </summary>
    /// <param name="thulloService">Delegation service for Thullo business logic.</param>
    /// <param name="sessionService">Service for creating a User Information object.</param>
    public BoardsController(
      ThulloService thulloService,
      ISessionService sessionService)
    {
      _thulloService = thulloService;
      _sessionService = sessionService;
    }

    /// <summary>
    /// Gets all boards.
    /// </summary>
    /// <returns>A collection of boards.</returns>
    [HttpGet]
    public ActionResult<List<BoardDto>> Get()
    {
      ISession session = _sessionService.GetSession(User);

      var boards = _thulloService.GetBoards(session).Select(b => BoardDto.FromBoard(b)).ToList();
      return Ok(boards);
    }

    /// <summary>
    /// Gets a single board.
    /// </summary>
    /// <param name="boardId">The list of the board.</param>
    /// <returns>A single board.</returns>
    [HttpGet]
    [Route("{boardId}")]
    public ActionResult<BoardDto> Get(int boardId)
    {
      ISession session = _sessionService.GetSession(User);

      var board = BoardDto.FromBoard(_thulloService.GetBoard(session, boardId));
      return Ok(board);
    }

    /// <summary>
    /// Creates a new board.
    /// </summary>
    /// <param name="dto">The representation of the new board to be created.</param>
    /// <returns>The newly created board.</returns>
    [HttpPost]
    public ActionResult<BoardDto> Post(BoardForCreationDto dto)
    {
      ISession session = _sessionService.GetSession(User);

      Board board = BoardForCreationDto.ToBoard(dto);

      var createdBoard = BoardDto.FromBoard(_thulloService.CreateBoard(session, board));
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
    public ActionResult<BoardDto> Put(int boardId, BoardForUpdateDto dto)
    {
      ISession session = _sessionService.GetSession(User);

      Board board = BoardForUpdateDto.ToBoard(dto);

      var updatedBoard = BoardDto.FromBoard(_thulloService.UpdateBoard(session, boardId, board));
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
      ISession session = _sessionService.GetSession(User);

      _thulloService.DeleteBoard(session, boardId);
      return Ok();
    }
  }
}