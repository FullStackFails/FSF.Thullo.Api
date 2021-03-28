using FSF.Thullo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace FSF.Thullo.Core.Interfaces.DataAccess
{
  public interface IThulloAuthRepository
  {
    BoardAccess CreateBoardAccess(IDbConnection connection, int boardId, Guid userId, bool canEdit, bool isOwner, IDbTransaction transaction);
    IEnumerable<BoardAccess> GetBoardAccessForUser(IDbConnection connection, Guid userId, IDbTransaction transaction);
    IEnumerable<Guid> GetBoardUsers(IDbConnection connection, int boardId, IDbTransaction transaction);
    bool CanViewBoard(IDbConnection connection, Guid userId, int boardId, IDbTransaction transaction);
    bool CanEditBoard(IDbConnection connection, Guid userId, int boardId, IDbTransaction transaction);
  }
}
