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

    public void Create(Board entity)
    {
      using(IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Title", entity.Title, DbType.String, ParameterDirection.Input, 100);
        parameters.Add("@CoverPhoto", entity.CoverPhoto, DbType.String, ParameterDirection.Input, 4000);
        parameters.Add("@IsPrivate", entity.IsPrivate, DbType.Byte, ParameterDirection.Input);

        var sql = @"INSERT INTO dbo.Board
                            (Title, CoverPhoto, IsPrivate)
                            VALUES(@Title, @CoverPhoto, @IsPrivate)";

        db.Execute(sql, parameters);
      }
    }

    public void Delete(int id)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

        var sql = @"DELETE dbo.Board
                      WHERE Id = @Id";

        db.Query(sql, parameters);
      }
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

    public Board Get(int id)
    {
      Board board = null;
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

        var sql = @"SELECT TOP 1 *
                      FROM dbo.Board
                      WHERE Id = @Id";

        board = db.QuerySingle<Board>(sql, parameters);
      }

      return board;
    }

    public void Update(Board entity)
    {
      using (IDbConnection db = new SqlConnection(connectionString))
      {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id, DbType.Int32, ParameterDirection.Input);
        parameters.Add("@Title", entity.Title, DbType.String, ParameterDirection.Input, 100);
        parameters.Add("@Description", entity.Description, DbType.String, ParameterDirection.Input, 4000);
        parameters.Add("@CoverPhoto", entity.CoverPhoto, DbType.String, ParameterDirection.Input, 4000);

        var sql = @"UPDATE dbo.Board
                      SET Title = @Title,
                      Description = @Description,
                      CoverPhoto = @CoverPhoto,
                      ModifiedDate = GetDate()
                      WHERE Id = @Id";

        db.Query(sql, parameters);
      }
    }
  }
}
