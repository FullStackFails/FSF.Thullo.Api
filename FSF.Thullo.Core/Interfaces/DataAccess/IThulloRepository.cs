using FSF.Thullo.Core.Entities;
using System.Collections.Generic;
using System.Data;

namespace FSF.Thullo.Core.Interfaces.DataAccess
{
  public interface IThulloRepository
  {
    // Boards
    Board CreateBoard(IDbConnection db, Board board);
    IEnumerable<Board> GetBoards(IDbConnection db);
    Board GetBoard(IDbConnection db, int boardId);
    Board UpdateBoard(IDbConnection db, int boardId, Board board);
    void DeleteBoard(IDbConnection db, int boardId);
    // Lists
    List CreateList(IDbConnection db, int boardId, List list);
    IEnumerable<List> GetLists(IDbConnection db, int boardId);
    List GetList(IDbConnection db, int boardId, int listId);
    List UpdateList(IDbConnection db, int boardId, int listId, List list);
    void DeleteList(IDbConnection db, int boardId, int listId);
    // Cards
    Card CreateCard(IDbConnection db, int boardId, int listId, Card card);
    IEnumerable<Card> GetCards(IDbConnection db, int boardId, int listId);
    Card GetCard(IDbConnection db, int boardId, int listId, int cardId);
    Card UpdateCard(IDbConnection db, int boardId, int listId, int cardId, Card card);
    void DeleteCard(IDbConnection db, int boardId, int listId, int cardId);
  }
}
