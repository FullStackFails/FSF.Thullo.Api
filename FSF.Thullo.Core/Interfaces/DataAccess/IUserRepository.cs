using FSF.Thullo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace FSF.Thullo.Core.Interfaces.DataAccess
{
  public interface IUserRepository
  {
    User GetUser(IDbConnection connection, Guid userId, IDbTransaction transaction = null);
    List<User> GetUsers(IDbConnection connection, List<Guid> userIds, IDbTransaction transaction = null);
  }
}
