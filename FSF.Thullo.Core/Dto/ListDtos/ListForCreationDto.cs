using FSF.Thullo.Core.Entities;

namespace FSF.Thullo.Core.Dto.ListDtos
{
  public class ListForCreationDto
  {
    public string Title { get; set; }
    public int BoardId { get; set; }

    public static List ToList(ListForCreationDto dto)
    {
      return new List
      {
        Title = dto.Title,
        BoardId = dto.BoardId
      };
    }

    public static ListForCreationDto FromList(List list)
    {
      return new ListForCreationDto
      {
        Title = list.Title,
        BoardId = list.BoardId
      };
    }
  }
}
