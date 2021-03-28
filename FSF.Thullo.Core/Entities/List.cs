using System;

namespace FSF.Thullo.Core.Entities
{
  public class List
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public int BoardId { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
  }
}
