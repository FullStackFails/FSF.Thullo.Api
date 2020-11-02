using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Services;

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

    // TODO: Put in separate Lists controller?
    [HttpGet]
    [Route("{listId}")]
    public IActionResult Get(int boardId, int listId)
    {
      return Ok(_thulloService.GetList(listId));
    }

    [HttpPost]
    public IActionResult Post(int boardId, List list)
    {
      return Created(string.Empty, _thulloService.CreateList(boardId, list));
    }

    // TODO: Put in separate Lists controller?
    [HttpPut]
    [Route("{listId}")]
    public IActionResult Put(int boardId, int listId, List list)
    {
      return Ok(_thulloService.UpdateList(listId, list));
    }

    // TODO: Put in separate Lists controller?
    [HttpDelete]
    [Route("{listId}")]
    public IActionResult Delete(int boardId, int listId)
    {
      _thulloService.DeleteList(listId);
      return Ok();
    }
  }
}