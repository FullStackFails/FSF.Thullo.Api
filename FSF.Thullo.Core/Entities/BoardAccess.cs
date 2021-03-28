using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSF.Thullo.Core.Entities
{
  public class BoardAccess
  {
    public int Id { get; set; }
    public int BoardId { get; set; }
    public Guid UserId { get; set; }
    public bool CanEdit { get; set; }
    public bool IsOwner { get; set; }
  }
}
