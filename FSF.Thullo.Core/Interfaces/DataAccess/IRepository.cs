using System;
using System.Collections.Generic;
using System.Text;

namespace FSF.Thullo.Core.Interfaces.DataAccess
{
  public interface IRepository<TEntity>
  {
    void Create(TEntity entity);
    IEnumerable<TEntity> Get();
    TEntity Get(int Id);
    TEntity Update(TEntity entity);
    void Delete(int Id);
  }
}
