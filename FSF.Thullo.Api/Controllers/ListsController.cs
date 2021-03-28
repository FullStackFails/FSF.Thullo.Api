using FSF.Thullo.Core.Dto.ListDtos;
using FSF.Thullo.Core.Interfaces.Security;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FSF.Thullo.Api.Controllers
{
  /// <summary>
  /// This controller provides an interface for working with lists.
  /// Lists have a many to one relationship with a given board.
  /// A list is a subresource of a board.
  /// </summary>
  [Route("api/boards/{boardId}/lists")]
  [ApiController]
  [Authorize]
  public class ListsController : ControllerBase
  {
    private readonly ThulloService _thulloService;
    private readonly ISessionService _sessionService;

    /// <summary>
    /// Constructor for the lists controller.
    /// </summary>
    /// <param name="thulloService">Delegation service for Thullo business logic.</param>
    /// <param name="sessionService">Service for creating a User Information object.</param>
    public ListsController(ThulloService thulloService, ISessionService sessionService)
    {
      _thulloService = thulloService;
      _sessionService = sessionService;
    }

    /// <summary>
    /// Gets all lists belonging to a board.
    /// </summary>
    /// <param name="boardId">The id of the board you want to get all lists for.</param>
    /// <returns>The lists that belong to the specified board.</returns>
    [HttpGet]
    public ActionResult<List<ListDto>> Get(int boardId)
    {
      ISession session = _sessionService.GetSession(User);

      var lists = _thulloService.GetLists(session, boardId).Select(l => ListDto.FromList(l));
      return Ok(lists);
    }

    /// <summary>
    /// Gets a single list belonging to a board.
    /// </summary>
    /// <param name="boardId">The id of the board you want to get a list for.</param>
    /// <param name="listId">The id of the list you want.</param>
    /// <returns>The list that belongs to the specified board.</returns>
    [HttpGet]
    [Route("{listId}")]
    public ActionResult<ListDto> Get(int boardId, int listId)
    {
      ISession session = _sessionService.GetSession(User);

      var list = ListDto.FromList(_thulloService.GetList(session, boardId, listId));
      return Ok(list);
    }

    /// <summary>
    /// Creates/Adds a new list associated to a board.
    /// </summary>
    /// <param name="boardId">The id of the board you want add a list to.</param>
    /// <param name="dto">The representation of the new list being created.</param>
    /// <returns>The newly created list.</returns>
    [HttpPost]
    public ActionResult<ListDto> Post(int boardId, ListForCreationDto dto)
    {
      ISession session = _sessionService.GetSession(User);

      var list = ListForCreationDto.ToList(dto);
      list.BoardId = boardId;

      var createdList = ListDto.FromList(_thulloService.CreateList(session, boardId, list));
      return Created(string.Empty, createdList);
    }

    /// <summary>
    /// Updates a single list belonging to a board.
    /// </summary>
    /// <param name="boardId">The id of the board that the list being updated belongs to.</param>
    /// <param name="listId">The id of the list that you want to update.</param>
    /// <param name="dto">The desired updated representation.</param>
    /// <returns>The updated representation of the list.</returns>
    [HttpPut]
    [Route("{listId}")]
    public ActionResult<ListDto> Put(int boardId, int listId, ListForUpdateDto dto)
    {
      ISession session = _sessionService.GetSession(User);

      var list = ListForUpdateDto.ToList(dto);
      list.BoardId = boardId;
      list.Id = listId;

      var updatedList = ListDto.FromList(_thulloService.UpdateList(session, boardId, listId, list));

      return Ok(updatedList);
    }

    /// <summary>
    /// Deletes a list from a given board.
    /// </summary>
    /// <param name="boardId">The id of the board that the list being deleted belongs to.</param>
    /// <param name="listId">The id of the list that you want to delete.</param>
    [HttpDelete]
    [Route("{listId}")]
    public IActionResult Delete(int boardId, int listId)
    {
      ISession session = _sessionService.GetSession(User);

      _thulloService.DeleteList(session, boardId, listId);
      return Ok();
    }
  }
}