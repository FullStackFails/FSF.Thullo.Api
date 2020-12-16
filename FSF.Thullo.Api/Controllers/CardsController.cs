using FSF.Thullo.Core.Dto.CardDtos;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FSF.Thullo.Api.Controllers
{
  [Route("api/boards/{boardId}/lists/{listId}/cards")]
  [ApiController]
  [Authorize]
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
      var cards = _thulloService.GetCards(boardId, listId).Select(c => CardDto.FromCard(c));

      return Ok(cards);
    }

    [HttpGet]
    [Route("{cardId}")]
    public IActionResult Get(int boardId, int listId, int cardId)
    {
      var card = CardDto.FromCard(_thulloService.GetCard(boardId, listId, cardId));
      return Ok(card);
    }

    [HttpPost]
    public IActionResult Post(int boardId, int listId, CardforCreationDto dto)
    {
      var card = CardforCreationDto.ToCard(dto);
      card.ListId = listId;

      var createdCard = CardDto.FromCard(_thulloService.CreateCard(boardId, listId, card));

      return Created(string.Empty, createdCard);
    }

    [HttpPut]
    [Route("{cardId}")]
    public IActionResult Put(int boardId, int listId, int cardId, CardForUpdateDto dto)
    {
      var card = CardForUpdateDto.ToCard(dto);
      card.ListId = listId;
      card.Id = cardId;

      var updatedCard = CardDto.FromCard(_thulloService.UpdateCard(boardId, listId, cardId, card));

      return Ok(updatedCard);
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