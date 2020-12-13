using FSF.Thullo.Core.Dto.ListDtos;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FSF.Thullo.Api.Controllers
{
  [Route("api/Boards/{boardId}/Lists")]
  [ApiController]
  public class ListsController : ControllerBase
  {

    private readonly ThulloService _thulloService;

    public ListsController(ThulloService thulloService)
    {
      _thulloService = thulloService;
    }

    /// <summary>
    /// Gets all lists belonging to a board.
    /// </summary>
    /// <param name="boardId">The id of the board you want to get all lists for.</param>
    /// <returns>The lists that belong to the specified board.</returns>
    [HttpGet]
    public IActionResult Get(int boardId)
    {
      var lists = _thulloService.GetLists(boardId).Select(l => ListDto.FromList(l));
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
    public IActionResult Get(int boardId, int listId)
    {
      var list = ListDto.FromList(_thulloService.GetList(boardId, listId));
      return Ok(list);
    }

    /// <summary>
    /// Creates/Adds a new list associated to a board.
    /// </summary>
    /// <param name="boardId">The id of the board you want add a list to.</param>
    /// <param name="dto">The representation of the new list being created.</param>
    /// <returns>The newly created list.</returns>
    [HttpPost]
    public IActionResult Post(int boardId, ListForCreationDto dto)
    {
      var list = ListForCreationDto.ToList(dto);
      list.BoardId = boardId;

      var createdList = ListDto.FromList(_thulloService.CreateList(boardId, list));
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
    public IActionResult Put(int boardId, int listId, ListForUpdateDto dto)
    {
      var list = ListForUpdateDto.ToList(dto);
      list.BoardId = boardId;
      list.Id = listId;

      var updatedList = ListDto.FromList(_thulloService.UpdateList(boardId, listId, list));

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
      _thulloService.DeleteList(boardId, listId);
      return Ok();
    }
  }
}