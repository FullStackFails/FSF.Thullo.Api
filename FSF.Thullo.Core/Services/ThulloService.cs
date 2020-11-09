using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.DataAccess;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FSF.Thullo.Core.Services
{
  public class ThulloService
  {
    private IThulloRepository _repository;
    private const string connectionString = @"Data Source=(LocalDb)\SQLSERVER;Initial Catalog=Thullo;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public ThulloService(IThulloRepository repository)
    {
      _repository = repository;
    }

    #region Boards
    public IEnumerable<Board> GetBoards()
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.GetBoards(db);
      }
    }

    public Board GetBoard(int boardId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.GetBoard(db, boardId);
      }
    }

    public Board CreateBoard(Board board)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.CreateBoard(db, board);
      }
    }

    public Board UpdateBoard(int boardId, Board board)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.UpdateBoard(db, boardId, board);
      }
    }

    public void DeleteBoard(int boardId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        _repository.DeleteBoard(db, boardId);
      }
    }
    #endregion

    #region lists
    public IEnumerable<List> GetLists(int boardId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.GetLists(db, boardId);
      }
    }

    public List GetList(int boardId, int listId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.GetList(db, boardId, listId);
      }
    }

    public List CreateList(int boardId, List list)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.CreateList(db, boardId, list);
      }
    }

    public List UpdateList(int boardId, int listId, List list)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.UpdateList(db, boardId, listId, list);
      }
    }

    public void DeleteList(int boardId, int listId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        _repository.DeleteList(db, boardId, listId);
      }
    }
    #endregion

    #region Cards
    public IEnumerable<Card> GetCards(int boardId, int listId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.GetCards(db, boardId, listId);
      }
    }

    public Card GetCard(int boardId, int listId, int cardId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.GetCard(db, boardId, listId, cardId);
      }
    }

    public Card CreateCard(int boardId, int listId, Card card)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.CreateCard(db, boardId, listId, card);
      }
    }

    public Card UpdateCard(int boardId, int listId, int cardId, Card card)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.UpdateCard(db, boardId, listId, cardId, card);
      }
    }

    public void DeleteCard(int boardId, int listId, int cardId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        _repository.DeleteCard(db, boardId, listId, cardId);
      }
    }
    #endregion
  }
}
