using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FSF.Thullo.Core.Services
{
  public class UserService
  {
    private readonly IUserRepository _userRepository;
    private const string _connectionString = @"Data Source=(LocalDb)\SQLSERVER;Initial Catalog=FsfUsers;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public UserService(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public User GetUserById(Guid userId)
    {
      using (IDbConnection connection = new SqlConnection(_connectionString))
      {
        User user = _userRepository.GetUser(connection, userId);

        if (user == null)
          throw new ArgumentOutOfRangeException(nameof(userId), $"User with userId: {userId} not found");

        return user;
      }
    }

    public List<User> GetUsersByIds(List<Guid> userIds)
    {
      using (IDbConnection connection = new SqlConnection(_connectionString))
      {
        List<User> users = _userRepository.GetUsers(connection, userIds);

        if (users == null)
          throw new ArgumentOutOfRangeException(nameof(userIds), $"Users with ids: {string.Join(",", userIds)} not found");

        return users;
      }
    }
  }
}
