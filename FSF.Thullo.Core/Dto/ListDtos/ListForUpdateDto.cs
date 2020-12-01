using FSF.Thullo.Core.Entities;

namespace FSF.Thullo.Core.Dto.ListDtos
{
  public class ListForUpdateDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public int BoardId { get; set; }

    public static List ToList(ListForUpdateDto dto)
    {
      return new List
      {
        Id = dto.Id,
        Title = dto.Title,
        BoardId = dto.BoardId
      };
    }

    public static ListForUpdateDto FromList(List list)
    {
      return new ListForUpdateDto
      {
        Id = list.Id,
        Title = list.Title,
        BoardId = list.BoardId
      };
    }
  }
}
