using FSF.Thullo.Core.Entities;

namespace FSF.Thullo.Core.Dto.CardDtos
{
  public class CardDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public int ListId { get; set; }

    public static Card ToCard(CardDto dto)
    {
      return new Card
      {
        Id = dto.Id,
        Title = dto.Title,
        Description = dto.Description,
        CoverImage = dto.CoverImage,
        ListId = dto.ListId
      };
    }

    public static CardDto FromCard(Card card)
    {
      return new CardDto
      {
        Id = card.Id,
        Title = card.Title,
        Description = card.Description,
        CoverImage = card.CoverImage,
        ListId = card.ListId
      };
    }
  }
}
