using FSF.Thullo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace FSF.Thullo.Core.Interfaces.DataAccess
{
  public interface IThulloAuthRepository
  {
    BoardAccess CreateBoardAccess(IDbConnection connection, int boardId, Guid userId, bool canEdit, bool isOwner, IDbTransaction transaction = null);
    IEnumerable<BoardAccess> GetBoardAccessForUser(IDbConnection connection, Guid userId, IDbTransaction transaction = null);
    IEnumerable<Guid> GetBoardUsers(IDbConnection connection, int boardId, IDbTransaction transaction = null);
    bool CanViewBoard(IDbConnection connection, Guid userId, int boardId, IDbTransaction transaction = null);
    bool CanEditBoard(IDbConnection connection, Guid userId, int boardId, IDbTransaction transaction = null);
  }
}
