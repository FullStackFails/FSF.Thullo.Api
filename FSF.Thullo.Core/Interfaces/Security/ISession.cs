using System;

namespace FSF.Thullo.Core.Interfaces.Security
{
  public interface ISession
  {
    Guid UserId { get; set; }
  }
}
