using FSF.Thullo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSF.Thullo.Core.Dto.BoardDtos
{
  public class BoardForUpdateDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverPhoto { get; set; }
    public bool IsPrivate { get; set; }

    public static BoardForUpdateDto FromBoard(Board board)
    {
      return new BoardForUpdateDto
      {
        Id = board.Id,
        Title = board.Title,
        Description = board.Description,
        CoverPhoto = board.CoverPhoto,
        IsPrivate = board.IsPrivate
      };
    }

    public static Board ToBoard(BoardForUpdateDto dto)
    {
      return new Board
      {
        Id = dto.Id,
        Title = dto.Title,
        Description = dto.Description,
        CoverPhoto = dto.CoverPhoto,
        IsPrivate = dto.IsPrivate
      };
    }
  }
}
