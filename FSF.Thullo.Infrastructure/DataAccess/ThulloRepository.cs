using Dapper;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.DataAccess;
using System.Collections.Generic;
using System.Data;

namespace FSF.Thullo.Infrastructure.DataAccess
{
  public class ThulloRepository : IThulloRepository
  {
    #region Boards
    public Board CreateBoard(IDbConnection db, Board board)
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

      return db.QuerySingle<Board>(sql, parameters);
    }

    public void DeleteBoard(IDbConnection db, int boardId)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Id", boardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"DELETE dbo.Board
                    WHERE Id = @Id";

      db.Query(sql, parameters);
    }

    public IEnumerable<Board> GetBoards(IDbConnection db)
    {
      var sql = "SELECT * FROM dbo.Board";

      return db.Query<Board>(sql);
    }

    public Board GetBoard(IDbConnection db, int boardId)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Id", boardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT TOP 1 *
                    FROM dbo.Board
                    WHERE Id = @Id";

      return db.QuerySingleOrDefault<Board>(sql, parameters);
    }

    public Board UpdateBoard(IDbConnection db, int boardId, Board board)
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

      return db.QuerySingleOrDefault<Board>(sql, parameters);
    }
    #endregion

    #region Lists
    public List CreateList(IDbConnection db, int boardId, List list)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Title", list.Title, DbType.String, ParameterDirection.Input, 100);
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"INSERT INTO dbo.List
                            (Title, BoardId)
                            VALUES(@Title, @BoardId)

                  SELECT TOP 1 * FROM dbo.List WHERE Id = SCOPE_IDENTITY()";

      return db.QuerySingle<List>(sql, parameters);
    }

    public void DeleteList(IDbConnection db, int boardId, int listId)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);

      var sql = @"DELETE dbo.List
                    WHERE BoardId = @BoardId
                    AND Id = @ListId";

      db.Query(sql, parameters);
    }

    public IEnumerable<List> GetLists(IDbConnection db, int boardId)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT * FROM dbo.List
                    WHERE BoardId = @BoardId";

      return db.Query<List>(sql, parameters);
    }

    public List GetList(IDbConnection db, int boardId, int listId)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@BoardId", boardId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT * FROM dbo.List
                    WHERE BoardId = @BoardId
                    AND Id = @ListId";

      return db.QuerySingleOrDefault<List>(sql, parameters);
    }

    public List UpdateList(IDbConnection db, int boardId, int listId, List list)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Id", listId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@Title", list.Title, DbType.String, ParameterDirection.Input, 100);

      var sql = @"UPDATE dbo.List
                      SET Title = @Title,
                      ModifiedDate = GetDate()
                      WHERE Id = @Id

                  SELECT TOP 1 * FROM dbo.List WHERE Id = @Id";

      return db.QuerySingleOrDefault<List>(sql, parameters);
    }
    #endregion

    #region Cards
    public Card CreateCard(IDbConnection db, int boardId, int listId, Card card)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Title", card.Title, DbType.String, ParameterDirection.Input, 100);
      parameters.Add("@Description", card.Description, DbType.String, ParameterDirection.Input, 4000);
      parameters.Add("@CoverImage", card.CoverImage, DbType.String, ParameterDirection.Input, 4000);
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);

      var sql = @"INSERT INTO dbo.Card
                            (Title, Description, CoverImage, ListId)
                            VALUES(@Title, @Description, @CoverImage, @ListId)

                  SELECT TOP 1 * FROM dbo.Card WHERE Id = SCOPE_IDENTITY()";

      return db.QuerySingle<Card>(sql, parameters);
    }

    public IEnumerable<Card> GetCards(IDbConnection db, int boardId, int listId)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT * FROM dbo.Card
                    WHERE ListId = @ListId";

      return db.Query<Card>(sql, parameters);
    }

    public Card GetCard(IDbConnection db, int boardId, int listId, int cardId)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@CardId", cardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"SELECT * FROM dbo.Card
                    WHERE ListId = @ListId
                    AND Id = @CardId";

      return db.QuerySingleOrDefault<Card>(sql, parameters);
    }

    public Card UpdateCard(IDbConnection db, int boardId, int listId, int cardId, Card card)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@Id", cardId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@Title", card.Title, DbType.String, ParameterDirection.Input, 100);
      parameters.Add("@Description", card.Description, DbType.String, ParameterDirection.Input, 4000);
      parameters.Add("@CoverImage", card.CoverImage, DbType.String, ParameterDirection.Input, 4000);

      var sql = @"UPDATE dbo.Card
                      SET Title = @Title,
                      Description = @Description,
                      CoverImage = @CoverImage,
                      ModifiedDate = GetDate()
                      WHERE Id = @Id

                  SELECT TOP 1 * FROM dbo.Card WHERE Id = @Id";

      return db.QuerySingleOrDefault<Card>(sql, parameters);
    }

    public void DeleteCard(IDbConnection db, int boardId, int listId, int cardId)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@ListId", listId, DbType.Int32, ParameterDirection.Input);
      parameters.Add("@CardId", cardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"DELETE dbo.Card
                    WHERE ListId = @ListId
                    AND Id = @CardId";

      db.Query(sql, parameters);
    }
    #endregion

    private bool BoardExists(IDbConnection db, int boardId)
    {
      bool exists = false;

      var parameters = new DynamicParameters();
      parameters.Add("@Id", boardId, DbType.Int32, ParameterDirection.Input);

      var sql = @"IF EXISTS (
	                  SELECT TOP 1 *
		                  FROM Board
		                  WHERE id = @Id)
	                  SELECT 1 AS found
                  ELSE
	                  SELECT 0 AS Found";

      exists = db.ExecuteScalar<bool>(sql, parameters);

      return exists;
    }

    private bool ListExists(IDbConnection db, int boardId, int listId)
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

      exists = db.ExecuteScalar<bool>(sql, parameters);

      return exists;
    }

    private bool CardExists(IDbConnection db, int boardId, int listId, int cardId)
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

      exists = db.ExecuteScalar<bool>(sql, parameters);

      return exists;
    }
  }
}
