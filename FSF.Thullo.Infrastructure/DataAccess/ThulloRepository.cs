using Dapper;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FSF.Thullo.Infrastructure.DataAccess
{
  public class ThulloRepository : IThulloRepository
  {
    private const string connectionString = @"Data Source=(LocalDb)\SQLSERVER;Initial Catalog=Thullo;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    #region Boards
    public Board CreateBoard(Board board)
    {
      Board createdBoard;

      using(IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Title", board.Title, DbType.String, ParameterDirection.Input, 100);
        parameters.Add("@Description", board.Description, DbType.String, ParameterDirection.Input, 4000);
        parameters.Add("@CoverPhoto", board.CoverPhoto, DbType.String, ParameterDirection.Input, 4000);
        parameters.Add("@IsPrivate", board.IsPrivate, DbType.Byte, ParameterDirection.Input);

        var sql = @"INSERT INTO dbo.Board
                            (Title, Description, CoverPhoto, IsPrivate)
                            VALUES(@Title, @Description, @CoverPhoto, @IsPrivate)

                    SELECT TOP 1 * FROM dbo.Board WHERE Id = SCOPE_IDENTITY()";

        createdBoard = db.QuerySingle<Board>(sql, parameters);
      }

      return createdBoard;
    }

    public void DeleteBoard(int boardId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", boardId, DbType.Int32, ParameterDirection.Input);

        var sql = @"DELETE dbo.Board
                      WHERE Id = @Id";

        db.Query(sql, parameters);
      }
    }

    public IEnumerable<Board> GetBoards()
    {
      IEnumerable<Board> boards = null;
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var sql = "SELECT * FROM dbo.Board";

        boards = db.Query<Board>(sql);
      }

      return boards;
    }

    public Board GetBoard(int boardId)
    {
      Board board = null;
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", boardId, DbType.Int32, ParameterDirection.Input);

        var sql = @"SELECT TOP 1 *
                      FROM dbo.Board
                      WHERE Id = @Id";

        board = db.QuerySingle<Board>(sql, parameters);
      }

      return board;
    }

    public Board UpdateBoard(int boardId, Board board)
    {
      Board updatedBoard;

      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", boardId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("@Title", board.Title, DbType.String, ParameterDirection.Input, 100);
        parameters.Add("@Description", board.Description, DbType.String, ParameterDirection.Input, 4000);
        parameters.Add("@CoverPhoto", board.CoverPhoto, DbType.String, ParameterDirection.Input, 4000);
        parameters.Add("@IsPrivate", board.IsPrivate, DbType.Boolean, ParameterDirection.Input);

        var sql = @"UPDATE dbo.Board
                      SET Title = @Title,
                      Description = @Description,
                      CoverPhoto = @CoverPhoto,
                      IsPrivate = @IsPrivate,
                      ModifiedDate = GetDate()
                      WHERE Id = @Id

                    SELECT TOP 1 * FROM dbo.Board WHERE Id = @Id";

        updatedBoard = db.QuerySingle<Board>(sql, parameters);
      }

      return updatedBoard;
    }
    #endregion

    #region Lists
    public List CreateList(int boardId, List list)
    {
      List createdList;

      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Title", list.Title, DbType.String, ParameterDirection.Input, 100);
        parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);

        var sql = @"INSERT INTO dbo.List
                            (Title, BoardId)
                            VALUES(@Title, @BoardId)

                    SELECT TOP 1 * FROM dbo.List WHERE Id = SCOPE_IDENTITY()";

        createdList = db.QuerySingle<List>(sql, parameters);
      }

      return createdList;
    }

    public void DeleteList(int listId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", listId, DbType.Int32, ParameterDirection.Input);

        var sql = @"DELETE dbo.List
                      WHERE Id = @Id";

        db.Query(sql, parameters);
      }
    }

    public IEnumerable<List> GetLists(int boardId)
    {
      IEnumerable<List> lists = null;
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);

        var sql = @"SELECT * FROM dbo.List
                      WHERE BoardId = @BoardId";

        lists = db.Query<List>(sql, parameters);
      }

      return lists;
    }

    public List GetList(int listId)
    {
      List list = null;
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", listId, DbType.Int32, ParameterDirection.Input);

        var sql = @"SELECT * FROM dbo.List
                      WHERE Id = @Id";

        list = db.QuerySingle<List>(sql, parameters);
      }

      return list;
    }

    public List UpdateList(int listId, List list)
    {
      List updatedList;

      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", listId, DbType.Int32, ParameterDirection.Input);
        parameters.Add("@Title", list.Title, DbType.String, ParameterDirection.Input, 100);

        var sql = @"UPDATE dbo.List
                      SET Title = @Title,
                      ModifiedDate = GetDate()
                      WHERE Id = @Id

                    SELECT TOP 1 * FROM dbo.List WHERE Id = @Id";

        updatedList = db.QuerySingle<List>(sql, parameters);
      }

      return updatedList;
    }
    #endregion
  }
}
