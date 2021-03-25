using System.Security.Claims;

namespace FSF.Thullo.Core.Interfaces.Security
{
  public interface ISessionService
  {
    ISession GetSession(ClaimsPrincipal claimsPrinciple);
  }
}
