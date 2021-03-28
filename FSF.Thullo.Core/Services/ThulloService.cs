using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.DataAccess;
using FSF.Thullo.Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FSF.Thullo.Core.Services
{
  public class ThulloService
  {
    private IThulloRepository _repository;
    private IThulloAuthRepository _authRepository;
    private const string connectionString = @"Data Source=(LocalDb)\SQLSERVER;Initial Catalog=Thullo;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public ThulloService(IThulloRepository repository,
      IThulloAuthRepository thulloAuthRepository)
    {
      _repository = repository;
      _authRepository = thulloAuthRepository;
    }

    #region Boards
    public IEnumerable<Board> GetBoards(ISession session)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.GetBoards(db);
      }
    }

    public Board GetBoard(ISession session, int boardId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        Board board = _repository.GetBoard(db, boardId);
        if (board == null)
          throw new ArgumentOutOfRangeException(nameof(boardId), $"Board with boardId: {boardId} not found");

        return board;
      }
    }

    public Board CreateBoard(ISession session, Board board)
    {
      Board createdBoard = null;

      using (IDbConnection db = new SqlConnection(connectionString))
      {
        db.Open();
        using (IDbTransaction transaction = db.BeginTransaction())
        {
          createdBoard = _repository.CreateBoard(db, session.UserId, board, transaction);

          _authRepository.CreateBoardAccess(db, createdBoard.Id, session.UserId, true, true, transaction);

          transaction.Commit();
        }
      }
      return createdBoard;
    }

    public Board UpdateBoard(ISession session, int boardId, Board board)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        Board updatedBoard = _repository.UpdateBoard(db, boardId, board);
        if (updatedBoard == null)
          throw new ArgumentOutOfRangeException(nameof(boardId), $"Board with boardId: {boardId} not found");

        return updatedBoard;
      }
    }

    public void DeleteBoard(ISession session, int boardId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        _repository.DeleteBoard(db, boardId);
      }
    }
    #endregion

    #region lists
    public IEnumerable<List> GetLists(ISession session, int boardId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.GetLists(db, boardId);
      }
    }

    public List GetList(ISession session, int boardId, int listId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        List list = _repository.GetList(db, boardId, listId);
        if (list == null)
          throw new ArgumentOutOfRangeException(nameof(listId), $"List with listId: {listId} not found");

        return list;
      }
    }

    public List CreateList(ISession session, int boardId, List list)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.CreateList(db, session.UserId, boardId, list);
      }
    }

    public List UpdateList(ISession session, int boardId, int listId, List list)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        List updatedList = _repository.UpdateList(db, boardId, listId, list);
        if (list == null)
          throw new ArgumentOutOfRangeException(nameof(listId), $"List with listId: {listId} not found");

        return updatedList;
      }
    }

    public void DeleteList(ISession session, int boardId, int listId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        _repository.DeleteList(db, boardId, listId);
      }
    }
    #endregion

    #region Cards
    public IEnumerable<Card> GetCards(ISession session, int boardId, int listId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.GetCards(db, boardId, listId);
      }
    }

    public Card GetCard(ISession session, int boardId, int listId, int cardId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        Card card = _repository.GetCard(db, boardId, listId, cardId);
        if (card == null)
          throw new ArgumentOutOfRangeException(nameof(cardId), $"Card with cardId: {cardId} not found");

        return card;
      }
    }

    public Card CreateCard(ISession session, int boardId, int listId, Card card)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        return _repository.CreateCard(db, session.UserId, boardId, listId, card);
      }
    }

    public Card UpdateCard(ISession session, int boardId, int listId, int cardId, Card card)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        Card updatedCard = _repository.UpdateCard(db, boardId, listId, cardId, card);
        if (card == null)
          throw new ArgumentOutOfRangeException(nameof(cardId), $"Card with cardId: {cardId} not found");

        return updatedCard;
      }
    }

    public void DeleteCard(ISession session, int boardId, int listId, int cardId)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        _repository.DeleteCard(db, boardId, listId, cardId);
      }
    }
    #endregion
  }
}
