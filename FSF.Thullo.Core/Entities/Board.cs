using System;

namespace FSF.Thullo.Core.Entities
{
  public class Board
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverPhoto { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
  }
}
