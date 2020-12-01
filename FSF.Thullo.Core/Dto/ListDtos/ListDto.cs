using FSF.Thullo.Core.Entities;

namespace FSF.Thullo.Core.Dto.ListDtos
{
  public class ListDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public int BoardId { get; set; }

    public static List ToList(ListDto dto)
    {
      return new List
      {
        Id = dto.Id,
        Title = dto.Title,
        BoardId = dto.BoardId
      };
    }

    public static ListDto FromList(List list)
    {
      return new ListDto
      {
        Id = list.Id,
        Title = list.Title,
        BoardId = list.BoardId
      };
    }
  }
}
