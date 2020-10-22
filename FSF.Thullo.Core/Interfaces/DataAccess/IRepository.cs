using System;
using System.Collections.Generic;
using System.Text;

namespace FSF.Thullo.Core.Interfaces.DataAccess
{
  public interface IRepository<TEntity>
    where TEntity: class
  {
    TEntity Create(TEntity entity);
    IEnumerable<TEntity> Get();
    TEntity Get(int id);
    TEntity Update(int id, TEntity entity);
    void Delete(int id);
  }
}
