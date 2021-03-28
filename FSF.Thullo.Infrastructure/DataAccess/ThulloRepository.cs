using Dapper;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace FSF.Thullo.Infrastructure.DataAccess
{
  public class ThulloRepository : IThulloRepository
  {
    #region Boards
    public Board CreateBoard(IDbConnection connection, Guid userId, Board board, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Title", board.Title, DbType.String, ParameterDirection.Input, 100);
      parameters.Add("@Description", board.Description, DbType.String, ParameterDirection.Input, 4000);
      parameters.Add("@CoverPhoto", board.CoverPhoto, DbType.String, ParameterDirection.Input, 4000);
      parameters.Add("@IsPrivate", board.IsPrivate, DbType.Byte, ParameterDirection.Input);
      parameters.Add("@CreatedBy", userId, DbType.Guid, ParameterDirection.Input);

      var sql = @"INSERT INTO dbo.Board
                            (Title, Description, CoverPhoto, IsPrivate, CreatedBy)
                            VALUES(@Title, @Description, @CoverPhoto, @IsPrivate, @CreatedBy)

                  SELECT TOP 1 * FROM dbo.Board WHERE Id = SCOPE_IDENTITY()";

      return connection.QuerySingle<Board>(sql, parameters, transaction);
    }

    public void DeleteBoard(IDbConnection connection, int boardId, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Id", boardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"DELETE dbo.Board
                    WHERE Id = @Id";

      connection.Query(sql, parameters, transaction);
    }

    public IEnumerable<Board> GetBoards(IDbConnection connection, IDbTransaction transaction = null)
    {
      var sql = "SELECT * FROM dbo.Board";

      return connection.Query<Board>(sql, transaction);
    }

    public Board GetBoard(IDbConnection connection, int boardId, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Id", boardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT TOP 1 *
                    FROM dbo.Board
                    WHERE Id = @Id";

      return connection.QuerySingleOrDefault<Board>(sql, parameters, transaction);
    }

    public Board UpdateBoard(IDbConnection connection, int boardId, Board board, IDbTransaction transaction = null)
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
                      IsPrivate = @IsPrivate
                      WHERE Id = @Id

                  SELECT TOP 1 * FROM dbo.Board WHERE Id = @Id";

      return connection.QuerySingleOrDefault<Board>(sql, parameters, transaction);
    }
    #endregion

    #region Lists
    public List CreateList(IDbConnection connection, Guid userId, int boardId, List list, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Title", list.Title, DbType.String, ParameterDirection.Input, 100);
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@CreatedBy", userId, DbType.Guid, ParameterDirection.Input);

      var sql = @"INSERT INTO dbo.List
                            (Title, BoardId, CreatedBy)
                            VALUES(@Title, @BoardId, @CreatedBy)

                  SELECT TOP 1 * FROM dbo.List WHERE Id = SCOPE_IDENTITY()";

      return connection.QuerySingle<List>(sql, parameters, transaction);
    }

    public void DeleteList(IDbConnection connection, int boardId, int listId, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);

      var sql = @"DELETE dbo.List
                    WHERE BoardId = @BoardId
                    AND Id = @ListId";

      connection.Query(sql, parameters, transaction);
    }

    public IEnumerable<List> GetLists(IDbConnection connection, int boardId, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT * FROM dbo.List
                    WHERE BoardId = @BoardId";

      return connection.Query<List>(sql, parameters, transaction);
    }

    public List GetList(IDbConnection connection, int boardId, int listId, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT * FROM dbo.List
                    WHERE BoardId = @BoardId
                    AND Id = @ListId";

      return connection.QuerySingleOrDefault<List>(sql, parameters, transaction);
    }

    public List UpdateList(IDbConnection connection, int boardId, int listId, List list, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Id", listId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@Title", list.Title, DbType.String, ParameterDirection.Input, 100);

      var sql = @"UPDATE dbo.List
                      SET Title = @Title
                      WHERE Id = @Id

                  SELECT TOP 1 * FROM dbo.List WHERE Id = @Id";

      return connection.QuerySingleOrDefault<List>(sql, parameters, transaction);
    }
    #endregion

    #region Cards
    public Card CreateCard(IDbConnection connection, Guid userId, int boardId, int listId, Card card, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Title", card.Title, DbType.String, ParameterDirection.Input, 100);
      parameters.Add("@Description", card.Description, DbType.String, ParameterDirection.Input, 4000);
      parameters.Add("@CoverImage", card.CoverImage, DbType.String, ParameterDirection.Input, 4000);
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@CreatedBy", userId, DbType.Guid, ParameterDirection.Input);

      var sql = @"INSERT INTO dbo.Card
                            (Title, Description, CoverImage, ListId, CreatedBy)
                            VALUES(@Title, @Description, @CoverImage, @ListId, @CreatedBy)

                  SELECT TOP 1 * FROM dbo.Card WHERE Id = SCOPE_IDENTITY()";

      return connection.QuerySingle<Card>(sql, parameters, transaction);
    }

    public IEnumerable<Card> GetCards(IDbConnection connection, int boardId, int listId, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT * FROM dbo.Card
                    WHERE ListId = @ListId";

      return connection.Query<Card>(sql, parameters, transaction);
    }

    public Card GetCard(IDbConnection connection, int boardId, int listId, int cardId, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@CardId", cardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT * FROM dbo.Card
                    WHERE ListId = @ListId
                    AND Id = @CardId";

      return connection.QuerySingleOrDefault<Card>(sql, parameters, transaction);
    }

    public Card UpdateCard(IDbConnection connection, int boardId, int listId, int cardId, Card card, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Id", cardId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@Title", card.Title, DbType.String, ParameterDirection.Input, 100);
      parameters.Add("@Description", card.Description, DbType.String, ParameterDirection.Input, 4000);
      parameters.Add("@CoverImage", card.CoverImage, DbType.String, ParameterDirection.Input, 4000);

      var sql = @"UPDATE dbo.Card
                      SET Title = @Title,
                      Description = @Description,
                      CoverImage = @CoverImage
                      WHERE Id = @Id

                  SELECT TOP 1 * FROM dbo.Card WHERE Id = @Id";

      return connection.QuerySingleOrDefault<Card>(sql, parameters, transaction);
    }

    public void DeleteCard(IDbConnection connection, int boardId, int listId, int cardId, IDbTransaction transaction = null)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@CardId", cardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"DELETE dbo.Card
                    WHERE ListId = @ListId
                    AND Id = @CardId";

      connection.Query(sql, parameters, transaction);
    }
    #endregion

    private bool BoardExists(IDbConnection connection, int boardId)
    {
      bool exists = false;

      var parameters = new DynamicParameters();
      parameters.Add("@Id", boardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"IF EXISTS (
	                  SELECT TOP 1 *
		                  FROM dbo.Board
		                  WHERE id = @Id)
	                  SELECT 1 AS found
                  ELSE
	                  SELECT 0 AS Found";

      exists = connection.ExecuteScalar<bool>(sql, parameters);

      return exists;
    }

    private bool ListExists(IDbConnection connection, int boardId, int listId)
    {
      bool exists = false;

      var parameters = new DynamicParameters();
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);

      var sql = @"IF EXISTS (
	                  SELECT TOP 1 *
		                  FROM dbo.List
		                  WHERE BoardId = @BoardId
                        AND Id = @ListId)
	                  SELECT 1 AS found
                  ELSE
	                  SELECT 0 AS Found";

      exists = connection.ExecuteScalar<bool>(sql, parameters);

      return exists;
    }

    private bool CardExists(IDbConnection connection, int boardId, int listId, int cardId)
    {
      bool exists = false;

      var parameters = new DynamicParameters();
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@CardId", cardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"IF EXISTS (
	                  SELECT TOP 1 *
		                  FROM dbo.Card
		                  WHERE ListId = @ListId
                        AND Id = @CardId)
	                  SELECT 1 AS found
                  ELSE
	                  SELECT 0 AS Found";

      exists = connection.ExecuteScalar<bool>(sql, parameters);

      return exists;
    }
  }
}
