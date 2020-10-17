using Dapper;
using FSF.Thullo.Core.Entities;
using FSF.Thullo.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FSF.Thullo.Infrastructure.DataAccess
{
  public class BoardRepository : IRepository<Board>
  {
    private const string connectionString = @"Data Source=(LocalDb)\SQLSERVER;Initial Catalog=Thullo;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    //private readonly string _connectionString = @"Data Source=(LocalDb)\SQLSERVER;Initial Catalog=HomeBrew;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    public void Create(Board entity)
    {
      using(IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Title", entity.Title, DbType.String, ParameterDirection.Input, 100);
        parameters.Add("@CoverPhoto", entity.CoverPhoto, DbType.String, ParameterDirection.Input, 4000);
        parameters.Add("@IsPrivate", entity.IsPrivate, DbType.Byte, ParameterDirection.Input);

        var insertSql = @"INSERT INTO dbo.Board
                            (Title, CoverPhoto, IsPrivate)
                            VALUES(@Title, @CoverPhoto, @IsPrivate)";

        db.Execute(insertSql, parameters);
      }
    }

    public void Delete(int Id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Board> Get()
    {
      IEnumerable<Board> boards = null;
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var sql = "SELECT * FROM dbo.Board";

        boards = db.Query<Board>(sql);
      }

      return boards;
    }

    public Board Get(int Id)
    {
      throw new NotImplementedException();
    }

    public Board Update(Board entity)
    {
      throw new NotImplementedException();
    }
  }
}
