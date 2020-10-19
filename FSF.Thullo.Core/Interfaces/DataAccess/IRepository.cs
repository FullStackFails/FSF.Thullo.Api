using System;
using System.Collections.Generic;
using System.Text;

namespace FSF.Thullo.Core.Interfaces.DataAccess
{
  public interface IRepository<TEntity>
    where TEntity: class
  {
    void Create(TEntity entity);
    IEnumerable<TEntity> Get();
    TEntity Get(int id);
    void Update(TEntity entity);
    void Delete(int id);
  }
}
