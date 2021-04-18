using FSF.Thullo.Core.Entities;
using System;
using System.Collections.Generic;

namespace FSF.Thullo.Core.Dto
{
  public class UserDto
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public static UserDto FromUser(User user)
    {
      return new UserDto
      {
        Id = Guid.Parse(user.Id),
        FirstName = user.FirstName,
        LastName = user.LastName
      };
    }

    public static List<UserDto> FromUsers(List<User> users)
    {
      List<UserDto> userDtos = new List<UserDto>();

      foreach(var user in users)
      {
        userDtos.Add(FromUser(user));
      }

      return userDtos;
    }
  }
}
