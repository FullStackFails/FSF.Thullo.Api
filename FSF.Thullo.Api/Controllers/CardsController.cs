using FSF.Thullo.Core.Dto.CardDtos;
using FSF.Thullo.Core.Interfaces.Security;
using FSF.Thullo.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FSF.Thullo.Api.Controllers
{
  /// <summary>
  /// This controller provides an interface for working with cards.
  /// Cards have a many to one relationship with a given list.
  /// A Card is a subresources of a List.
  /// </summary>
  [Route("api/boards/{boardId}/lists/{listId}/cards")]
  [ApiController]
  [Authorize]
  public class CardsController : ControllerBase
  {
    private readonly ThulloService _thulloService;
    private readonly ISessionService _sessionService;

    /// <summary>
    /// Constructor for the cards controller.
    /// </summary>
    /// <param name="thulloService">Delegation service for Thullo business logic.</param>
    /// <param name="sessionService">Service for creating a User Information object.</param>
    public CardsController(ThulloService thulloService, ISessionService sessionService)
    {
      _thulloService = thulloService;
      _sessionService = sessionService;
    }

    /// <summary>
    /// Gets all cards belonging to a list.
    /// </summary>
    /// <param name="boardId">The id of the board.</param>
    /// <param name="listId">The id of the list.</param>
    /// <returns>A collection of cards.</returns>
    [HttpGet]
    public ActionResult<List<CardDto>> Get(int boardId, int listId)
    {
      ISession session = _sessionService.GetSession(User);

      var cards = _thulloService.GetCards(boardId, listId).Select(c => CardDto.FromCard(c)).ToList();

      return Ok(cards);
    }

    /// <summary>
    /// Gets a single card.
    /// </summary>
    /// <param name="boardId">The id of the board.</param>
    /// <param name="listId">The id of the list.</param>
    /// <param name="cardId">The id of the card.</param>
    /// <returns>A single card.</returns>
    [HttpGet]
    [Route("{cardId}")]
    public ActionResult<CardDto> Get(int boardId, int listId, int cardId)
    {
      ISession session = _sessionService.GetSession(User);

      var card = CardDto.FromCard(_thulloService.GetCard(boardId, listId, cardId));
      return Ok(card);
    }

    /// <summary>
    /// Creates a new card associated to a list.
    /// </summary>
    /// <param name="boardId">The id of the board.</param>
    /// <param name="listId">The id of the list.</param>
    /// <param name="dto">The representation of the new card.</param>
    /// <returns>The newly created card.</returns>
    [HttpPost]
    public ActionResult<CardDto> Post(int boardId, int listId, CardforCreationDto dto)
    {
      ISession session = _sessionService.GetSession(User);

      var card = CardforCreationDto.ToCard(dto);
      card.ListId = listId;

      var createdCard = CardDto.FromCard(_thulloService.CreateCard(boardId, listId, card));

      return Created(string.Empty, createdCard);
    }

    /// <summary>
    /// Updates a card.
    /// </summary>
    /// <param name="boardId">The id of the board.</param>
    /// <param name="listId">The id of the list.</param>
    /// <param name="cardId">The id of the card.</param>
    /// <param name="dto">The updated representation of the card.</param>
    /// <returns>The updated card.</returns>
    [HttpPut]
    [Route("{cardId}")]
    public ActionResult<CardDto> Put(int boardId, int listId, int cardId, CardForUpdateDto dto)
    {
      ISession session = _sessionService.GetSession(User);

      var card = CardForUpdateDto.ToCard(dto);
      card.ListId = listId;
      card.Id = cardId;

      var updatedCard = CardDto.FromCard(_thulloService.UpdateCard(boardId, listId, cardId, card));

      return Ok(updatedCard);
    }

    /// <summary>
    /// Deletes a card.
    /// </summary>
    /// <param name="boardId">The id of the board.</param>
    /// <param name="listId">The id of the list.</param>
    /// <param name="cardId">The id of the card.</param>
    [HttpDelete]
    [Route("{cardId}")]
    public IActionResult Delete(int boardId, int listId, int cardId)
    {
      ISession session = _sessionService.GetSession(User);

      _thulloService.DeleteCard(boardId, listId, cardId);
      return Ok();
    }
  }
}