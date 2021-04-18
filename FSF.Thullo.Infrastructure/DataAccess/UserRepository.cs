using Dapper;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FSF.Thullo.Infrastructure.DataAccess
{
  public class UserRepository : IUserRepository
  {
    public User GetUser(IDbConnection connection, Guid userId, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Id", userId.ToString(), DbType.String, ParameterDirection.Input);

      var sql = @"SELECT Id, FirstName, LastName" +
                " FROM dbo.AspNetUsers" +
                " WHERE Id = @id";

      var user = connection.QuerySingleOrDefault<User>(sql, parameters, transaction);

      return user;
    }

    public List<User> GetUsers(IDbConnection connection, List<Guid> userIds, IDbTransaction transaction = null)
    {
      // reference: https://dapper-tutorial.net/parameter-list
      var sql = @"SELECT Id, FirstName, LastName FROM dbo.AspNetUsers WHERE Id IN @Ids;";

      var users = connection.Query<User>(sql, new { Ids = userIds }, transaction).ToList();

      return users;
    }
  }
}
