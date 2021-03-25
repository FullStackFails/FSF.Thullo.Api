using FSF.Thullo.Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSF.Thullo.Infrastructure.Security
{
  public class UserSession : ISession
  {
    public Guid UserId { get; set; }
  }
}
