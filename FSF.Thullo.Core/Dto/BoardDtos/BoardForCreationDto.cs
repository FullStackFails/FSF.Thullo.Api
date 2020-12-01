using FSF.Thullo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSF.Thullo.Core.Dto.BoardDtos
{
  public class BoardForCreationDto
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverPhoto { get; set; }
    public bool IsPrivate { get; set; }

    public static BoardForCreationDto FromBoard(Board board)
    {
      return new BoardForCreationDto
      {
        Title = board.Title,
        Description = board.Description,
        CoverPhoto = board.CoverPhoto,
        IsPrivate = board.IsPrivate
      };
    }

    public static Board ToBoard(BoardForCreationDto dto)
    {
      return new Board
      {
        Title = dto.Title,
        Description = dto.Description,
        CoverPhoto = dto.CoverPhoto,
        IsPrivate = dto.IsPrivate
      };
    }
  }
}
