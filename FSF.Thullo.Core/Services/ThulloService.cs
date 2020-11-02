using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSF.Thullo.Core.Services
{
  public class ThulloService
  {
    private IThulloRepository _repository;

    public ThulloService(IThulloRepository repository)
    {
      _repository = repository;
    }

    #region Boards
    public IEnumerable<Board> GetBoards()
    {
      return _repository.GetBoards();
    }

    public Board GetBoard(int boardId)
    {
      return _repository.GetBoard(boardId);
    }

    public Board CreateBoard(Board board)
    {
      return _repository.CreateBoard(board);
    }

    public Board UpdateBoard(int boardId, Board board)
    {
      return _repository.UpdateBoard(boardId, board);
    }

    public void DeleteBoard(int boardId)
    {
      _repository.DeleteBoard(boardId);
    }
    #endregion

    #region lists
    public IEnumerable<List> GetLists(int boardId)
    {
      return _repository.GetLists(boardId);
    }

    public List GetList(int listId)
    {
      return _repository.GetList(listId);
    }

    public List CreateList(int boardId, List list)
    {
      return _repository.CreateList(boardId, list);
    }

    public List UpdateList(int listId, List list)
    {
      return _repository.UpdateList(listId, list);
    }

    public void DeleteList(int listId)
    {
      _repository.DeleteList(listId);
    }
    #endregion
  }
}
