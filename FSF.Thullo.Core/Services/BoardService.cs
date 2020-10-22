using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSF.Thullo.Core.Services
{
  public class BoardService
  {
    private IRepository<Board> _repository;

    public BoardService(IRepository<Board> repository)
    {
      _repository = repository;
    }

    public IEnumerable<Board> Get()
    {
      return _repository.Get();
    }

    public Board Get(int id)
    {
      return _repository.Get(id);
    }

    public Board Create(Board board)
    {
      return _repository.Create(board);
    }

    public Board Update(int id, Board board)
    {
      return _repository.Update(id, board);
    }

    public void Delete(int id)
    {
      _repository.Delete(id);
    }
  }
}
