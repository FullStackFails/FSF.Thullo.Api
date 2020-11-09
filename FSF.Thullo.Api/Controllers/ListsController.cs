using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Mvc;

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
      return Ok(_thulloService.GetLists(boardId));
    }

    [HttpGet]
    [Route("{listId}")]
    public IActionResult Get(int boardId, int listId)
    {
      return Ok(_thulloService.GetList(boardId, listId));
    }

    [HttpPost]
    public IActionResult Post(int boardId, List list)
    {
      return Created(string.Empty, _thulloService.CreateList(boardId, list));
    }

    [HttpPut]
    [Route("{listId}")]
    public IActionResult Put(int boardId, int listId, List list)
    {
      return Ok(_thulloService.UpdateList(boardId, listId, list));
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