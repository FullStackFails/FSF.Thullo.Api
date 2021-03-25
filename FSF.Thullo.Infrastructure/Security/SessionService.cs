using FSF.Thullo.Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FSF.Thullo.Infrastructure.Security
{
  public class SessionService : ISessionService
  {
    public ISession GetSession(ClaimsPrincipal claimsPrinciple)
    {
      var userId = Guid.Parse(claimsPrinciple.Claims.SingleOrDefault(c => c.Type == "sub").Value);

      UserSession session = new UserSession
      {
        UserId = userId
      };

      return session;
    }
  }
}
