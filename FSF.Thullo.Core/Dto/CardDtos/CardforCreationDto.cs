using FSF.Thullo.Core.Entities;

namespace FSF.Thullo.Core.Dto.CardDtos
{
  public class CardforCreationDto
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public int ListId { get; set; }

    public static Card ToCard(CardforCreationDto dto)
    {
      return new Card
      {
        Title = dto.Title,
        Description = dto.Description,
        CoverImage = dto.CoverImage,
        ListId = dto.ListId
      };
    }

    public static CardforCreationDto FromCard(Card card)
    {
      return new CardforCreationDto
      {
        Title = card.Title,
        Description = card.Description,
        CoverImage = card.CoverImage,
        ListId = card.ListId
      };
    }
  }
}
