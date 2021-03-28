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
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        return _repository.GetBoards(connection, session.UserId);
      }
    }

    public Board GetBoard(ISession session, int boardId)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckViewRights(connection, session.UserId, boardId);

        Board board = _repository.GetBoard(connection, boardId);
        if (board == null)
          throw new ArgumentOutOfRangeException(nameof(boardId), $"Board with boardId: {boardId} not found");

        return board;
      }
    }

    public Board CreateBoard(ISession session, Board board)
    {
      Board createdBoard = null;

      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();
        using (IDbTransaction transaction = connection.BeginTransaction())
        {
          createdBoard = _repository.CreateBoard(connection, session.UserId, board, transaction);

          _authRepository.CreateBoardAccess(connection, createdBoard.Id, session.UserId, true, true, transaction);

          transaction.Commit();
        }
      }
      return createdBoard;
    }

    public Board UpdateBoard(ISession session, int boardId, Board board)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckEditRights(connection, session.UserId, boardId);

        Board updatedBoard = _repository.UpdateBoard(connection, boardId, board);
        if (updatedBoard == null)
          throw new ArgumentOutOfRangeException(nameof(boardId), $"Board with boardId: {boardId} not found");

        return updatedBoard;
      }
    }

    public void DeleteBoard(ISession session, int boardId)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckEditRights(connection, session.UserId, boardId);

        _repository.DeleteBoard(connection, boardId);
      }
    }
    #endregion

    #region lists
    public IEnumerable<List> GetLists(ISession session, int boardId)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckViewRights(connection, session.UserId, boardId);

        return _repository.GetLists(connection, boardId);
      }
    }

    public List GetList(ISession session, int boardId, int listId)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckViewRights(connection, session.UserId, boardId);

        List list = _repository.GetList(connection, boardId, listId);
        if (list == null)
          throw new ArgumentOutOfRangeException(nameof(listId), $"List with listId: {listId} not found");

        return list;
      }
    }

    public List CreateList(ISession session, int boardId, List list)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckEditRights(connection, session.UserId, boardId);

        return _repository.CreateList(connection, session.UserId, boardId, list);
      }
    }

    public List UpdateList(ISession session, int boardId, int listId, List list)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckEditRights(connection, session.UserId, boardId);

        List updatedList = _repository.UpdateList(connection, boardId, listId, list);
        if (list == null)
          throw new ArgumentOutOfRangeException(nameof(listId), $"List with listId: {listId} not found");

        return updatedList;
      }
    }

    public void DeleteList(ISession session, int boardId, int listId)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckEditRights(connection, session.UserId, boardId);

        _repository.DeleteList(connection, boardId, listId);
      }
    }
    #endregion

    #region Cards
    public IEnumerable<Card> GetCards(ISession session, int boardId, int listId)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckViewRights(connection, session.UserId, boardId);

        return _repository.GetCards(connection, boardId, listId);
      }
    }

    public Card GetCard(ISession session, int boardId, int listId, int cardId)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckViewRights(connection, session.UserId, boardId);

        Card card = _repository.GetCard(connection, boardId, listId, cardId);
        if (card == null)
          throw new ArgumentOutOfRangeException(nameof(cardId), $"Card with cardId: {cardId} not found");

        return card;
      }
    }

    public Card CreateCard(ISession session, int boardId, int listId, Card card)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckEditRights(connection, session.UserId, boardId);

        return _repository.CreateCard(connection, session.UserId, boardId, listId, card);
      }
    }

    public Card UpdateCard(ISession session, int boardId, int listId, int cardId, Card card)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckEditRights(connection, session.UserId, boardId);

        Card updatedCard = _repository.UpdateCard(connection, boardId, listId, cardId, card);
        if (card == null)
          throw new ArgumentOutOfRangeException(nameof(cardId), $"Card with cardId: {cardId} not found");

        return updatedCard;
      }
    }

    public void DeleteCard(ISession session, int boardId, int listId, int cardId)
    {
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        // Authorization
        CheckEditRights(connection, session.UserId, boardId);

        _repository.DeleteCard(connection, boardId, listId, cardId);
      }
    }
    #endregion

    private void CheckViewRights(IDbConnection connection, Guid userId, int boardId, IDbTransaction transaction = null)
    {
      bool canView = _authRepository.CanViewBoard(connection, userId, boardId, transaction);
      if (!canView)
        throw new UnauthorizedAccessException("You are not authorized to view this board.");
    }

    private void CheckEditRights(IDbConnection connection, Guid userId, int boardId, IDbTransaction transaction = null)
    {
      bool canEdit = _authRepository.CanEditBoard(connection, userId, boardId, transaction);
      if (!canEdit)
        throw new UnauthorizedAccessException("You are not authorized to edit this board.");
    }
  }
}
