using Dapper;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSF.Thullo.Infrastructure.DataAccess
{
  public class ThulloAuthRepository : IThulloAuthRepository
  {
    public bool CanEditBoard(IDbConnection connection, Guid userId, int boardId, IDbTransaction transaction)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@UserId", userId, DbType.Guid, ParameterDirection.Input);
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT CASE WHEN EXISTS (
                  	SELECT TOP 1 *
                  	FROM dbo.BoardAccess
                  	WHERE UserId = @UserId
                    AND BoardId = @BoardId
                    AND CanEdit = 1
                  )
                  THEN CAST(1 AS BIT)
                  ELSE CAST (0 AS BIT) END";

      return connection.QuerySingleOrDefault<bool>(sql, parameters, transaction);
    }

    public bool CanViewBoard(IDbConnection connection, Guid userId, int boardId, IDbTransaction transaction)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@UserId", userId, DbType.Guid, ParameterDirection.Input);
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT CASE WHEN EXISTS (
                  	SELECT TOP 1 *
                  	FROM dbo.BoardAccess
                  	WHERE UserId = @UserId
                    AND BoardId = @BoardId
                  )
                  THEN CAST(1 AS BIT)
                  ELSE CAST (0 AS BIT) END";

      return connection.QuerySingleOrDefault<bool>(sql, parameters, transaction);
    }

    public BoardAccess CreateBoardAccess(IDbConnection connection, int boardId, Guid userId, bool canEdit, bool isOwner, IDbTransaction transaction)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@UserId", userId, DbType.Guid, ParameterDirection.Input);
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@CanEdit", canEdit, DbType.Boolean, ParameterDirection.Input);
      parameters.Add("@IsOwner", isOwner, DbType.Boolean, ParameterDirection.Input);

      var sql = @"INSERT INTO dbo.BoardAccess
                  (BoardId, UserId, CanEdit, IsOwner)
                  VALUES (@BoardId, @UserId, @CanEdit, @IsOwner)

                  SELECT TOP 1 * FROM dbo.BoardAccess WHERE Id = SCOPE_IDENTITY()";

      return connection.QuerySingle<BoardAccess>(sql, parameters, transaction);
    }

    public IEnumerable<BoardAccess> GetBoardAccessForUser(IDbConnection connection, Guid userId, IDbTransaction transaction)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Guid> GetBoardUsers(IDbConnection connection, int boardId, IDbTransaction transaction)
    {
      throw new NotImplementedException();
    }
  }
}
