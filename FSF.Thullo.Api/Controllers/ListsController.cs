using FSF.Thullo.Core.Dto.ListDtos;
using FSF.Thullo.Core.Entities;
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


    [HttpGet]
    public IActionResult Get(int boardId)
    {
      var lists = _thulloService.GetLists(boardId).Select(l => ListDto.FromList(l));
      return Ok(lists);
    }

    [HttpGet]
    [Route("{listId}")]
    public IActionResult Get(int boardId, int listId)
    {
      var list = ListDto.FromList(_thulloService.GetList(boardId, listId));
      return Ok(list);
    }

    [HttpPost]
    public IActionResult Post(int boardId, ListForCreationDto dto)
    {
      var list = ListForCreationDto.ToList(dto);
      list.BoardId = boardId;

      var createdList = ListDto.FromList(_thulloService.CreateList(boardId, list));
      return Created(string.Empty, createdList);
    }

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

    [HttpDelete]
    [Route("{listId}")]
    public IActionResult Delete(int boardId, int listId)
    {
      _thulloService.DeleteList(boardId, listId);
      return Ok();
    }
  }
}