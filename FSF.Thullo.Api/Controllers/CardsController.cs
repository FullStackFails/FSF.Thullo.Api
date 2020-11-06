using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FSF.Thullo.Api.Controllers
{
  [Route("api/boards/{boardId}/lists/{listId}/cards")]
  [ApiController]
  public class CardsController : ControllerBase
  {
    private readonly ThulloService _thulloService;

    public CardsController(ThulloService thulloService)
    {
      _thulloService = thulloService;
    }

    [HttpGet]
    public IActionResult Get(int boardId, int listId)
    {
      return Ok(_thulloService.GetCards(boardId, listId));
    }

    [HttpGet]
    [Route("{cardId}")]
    public IActionResult Get(int boardId, int listId, int cardId)
    {
      return Ok(_thulloService.GetCard(boardId, listId, cardId));
    }

    [HttpPost]
    public IActionResult Post(int boardId, int listId, Card card)
    {
      return Created(string.Empty, _thulloService.CreateCard(boardId, listId, card));
    }

    [HttpPut]
    [Route("{cardId}")]
    public IActionResult Put(int boardId, int listId, int cardId, Card card)
    {
      return Ok(_thulloService.UpdateCard(boardId, listId, cardId, card));
    }

    [HttpDelete]
    [Route("{cardId}")]
    public IActionResult Delete(int boardId, int listId, int cardId)
    {
      _thulloService.DeleteCard(boardId, listId, cardId);
      return Ok();
    }
  }
}