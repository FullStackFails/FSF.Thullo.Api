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

    public void Create(Board board)
    {
      _repository.Create(board);
    }
  }
}
