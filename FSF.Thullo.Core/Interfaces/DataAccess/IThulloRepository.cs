using FSF.Thullo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSF.Thullo.Core.Interfaces.DataAccess
{
  public interface IThulloRepository
  {
    // Boards
    Board CreateBoard(Board board);
    IEnumerable<Board> GetBoards();
    Board GetBoard(int boardId);
    Board UpdateBoard(int boardId, Board board);
    void DeleteBoard(int boardId);
    // Lists
    List CreateList(int boardId, List list);
    IEnumerable<List> GetLists(int boardId);
    List GetList(int listId);
    List UpdateList(int listId, List list);
    void DeleteList(int listId);
  }
}
