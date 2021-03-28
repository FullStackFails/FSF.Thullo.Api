using FSF.Thullo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace FSF.Thullo.Core.Interfaces.DataAccess
{
  public interface IThulloRepository
  {
    // Boards
    Board CreateBoard(IDbConnection connection, Guid userId, Board board, IDbTransaction transaction = null);
    IEnumerable<Board> GetBoards(IDbConnection connection, IDbTransaction transaction = null);
    Board? GetBoard(IDbConnection connection, int boardId, IDbTransaction transaction = null);
    Board? UpdateBoard(IDbConnection connection, int boardId, Board board, IDbTransaction transaction = null);
    void DeleteBoard(IDbConnection connection, int boardId, IDbTransaction transaction = null);
    // Lists
    List CreateList(IDbConnection connection, Guid userId, int boardId, List list, IDbTransaction transaction = null);
    IEnumerable<List> GetLists(IDbConnection connection, int boardId, IDbTransaction transaction = null);
    List? GetList(IDbConnection connection, int boardId, int listId, IDbTransaction transaction = null);
    List? UpdateList(IDbConnection connection, int boardId, int listId, List list, IDbTransaction transaction = null);
    void DeleteList(IDbConnection connection, int boardId, int listId, IDbTransaction transaction = null);
    // Cards
    Card CreateCard(IDbConnection connection, Guid userId, int boardId, int listId, Card card, IDbTransaction transaction = null);
    IEnumerable<Card> GetCards(IDbConnection connection, int boardId, int listId, IDbTransaction transaction = null);
    Card? GetCard(IDbConnection connection, int boardId, int listId, int cardId, IDbTransaction transaction = null);
    Card? UpdateCard(IDbConnection connection, int boardId, int listId, int cardId, Card card, IDbTransaction transaction = null);
    void DeleteCard(IDbConnection connection, int boardId, int listId, int cardId, IDbTransaction transaction = null);
  }
}
