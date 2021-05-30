using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLot.Dal.Models;
using Microsoft.Data.SqlClient;

namespace AutoLot.Dal.DataOperations
{
  public class InventoryDal : IDisposable
  {
    private readonly string _connectionString;
    private SqlConnection _sqlConnection;
    private bool _disposed = false;

    public InventoryDal(string connectionString) => _connectionString =
      connectionString ?? throw new ArgumentNullException(paramName: nameof(connectionString));

    public InventoryDal() : this(
      "Data Source=(localdb)\\mssqllocaldb;Integrated Security=true; Initial Catalog=AutoLot")
    {
    }

    private void OpenConnection()
    {
      _sqlConnection = new SqlConnection
      {
        ConnectionString = _connectionString
      };

      _sqlConnection.Open();
    }

    private void CloseConnection()
    {
      if (_sqlConnection?.State != ConnectionState.Closed)
      {
        _sqlConnection?.Close();
      }
    }


    protected virtual void Dispose(bool disposing)
    {
      if (_disposed)
      {
        return;
      }

      if (disposing)
      {
        _sqlConnection.Dispose();
      }

      _disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public List<CarViewModel> GetAllInventory()
    {
      OpenConnection();

      List<CarViewModel> inventory = new();

      string sql = @"SELECT i.Id, i.Color, i.PetName,m.Name as Make
          FROM Inventory i
          INNER JOIN Make m on m.Id = i.MakeId";

      using SqlCommand command = new(sql, _sqlConnection) {CommandType = CommandType.Text};

      SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

      while (dataReader.Read())
      {
        inventory.Add(new CarViewModel
        {
          Id = (int) dataReader["Id"],
          Color = (string) dataReader["Color"],
          Make = (string) dataReader["Make"],
          PetName = (string) dataReader["PetName"]
        });
      }

      dataReader.Close();

      return inventory;
    }

    public CarViewModel GetCar(int id)
    {
      OpenConnection();

      CarViewModel car = null;

      SqlParameter param = new()
      {
        ParameterName = "@p_CarId",
        Value = id,
        SqlDbType = SqlDbType.Int,
        Direction = ParameterDirection.Input
      };

      string sql = @"SELECT i.Id, i.Color, i.PetName,m.Name as Make
          FROM Inventory i
          INNER JOIN Make m on m.Id = i.MakeId
          WHERE i.Id = @p_CarId";

      using SqlCommand command = new(sql, _sqlConnection) {CommandType = CommandType.Text};
      command.Parameters.Add(param);

      SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

      while (dataReader.Read())
      {
        car = new()
        {
          Id = (int) dataReader["Id"],
          Color = (string) dataReader["Color"],
          Make = (string) dataReader["Make"],
          PetName = (string) dataReader["PetName"]
        };
      }

      dataReader.Close();

      return car;
    }

    public void InsertAuto(string color, int makeId, string petName)
    {
      OpenConnection();

      string sql = "Insert Into Inventory (MakeId, Color, PetName) Values (@p_MakeId, @p_Color, @p_PetName)";

      using (SqlCommand command = new(sql, _sqlConnection))
      {
        SqlParameter paramMakeId = new()
        {
          ParameterName = "@p_MakeId", Value = makeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input
        };
        SqlParameter paramColor = new()
        {
          ParameterName = "@p_Color", Value = color, SqlDbType = SqlDbType.NVarChar, Size = 50,
          Direction = ParameterDirection.Input
        };
        SqlParameter paramPetName = new()
        {
          ParameterName = "@p_PetName", Value = petName, SqlDbType = SqlDbType.NVarChar, Size = 50,
          Direction = ParameterDirection.Input
        };

        command.CommandType = CommandType.Text;
        command.Parameters.Add(paramMakeId);
        command.Parameters.Add(paramColor);
        command.Parameters.Add(paramPetName);

        command.ExecuteNonQuery();
      }

      CloseConnection();
    }

    public void DeleteCar(int id)
    {
      OpenConnection();

      SqlParameter param = new()
      {
        ParameterName = "@p_CarId",
        Value = id,
        SqlDbType = SqlDbType.Int,
        Direction = ParameterDirection.Input
      };

      string sql = "Delete from Inventory where Id = @p_CarId";

      using (SqlCommand command = new(sql, _sqlConnection))
      {
        try
        {
          command.Parameters.Add(param);
          command.CommandType = CommandType.Text;
          command.ExecuteNonQuery();
        }
        catch (SqlException e)
        {
          Exception error = new("Sorry! That car is on order!", e);
          throw error;
        }
      }

      CloseConnection();
    }

    public void UpdateCarPetName(int id, string newPetName)
    {
      OpenConnection();

      SqlParameter paramId = new()
        {ParameterName = "@p_CarId", Value = id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input};
      SqlParameter paramName = new()
      {
        ParameterName = "@p_PetName", Value = newPetName, SqlDbType = SqlDbType.NVarChar, Size = 50,
        Direction = ParameterDirection.Input
      };

      string sql = "Update Inventory Set PetName = @p_PetName Where Id = @p_CarId";

      using (SqlCommand command = new(sql, _sqlConnection))
      {
        command.Parameters.Add(paramId);
        command.Parameters.Add(paramName);
        command.ExecuteNonQuery();
      }

      CloseConnection();
    }

    public string LookUpPetName(int carId)
    {
      OpenConnection();

      string petName;

      using (SqlCommand command = new("GetPetName", _sqlConnection))
      {
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter paramCarId = new()
          {ParameterName = "@p_CarId", SqlDbType = SqlDbType.Int, Value = carId, Direction = ParameterDirection.Input};

        command.Parameters.Add(paramCarId);

        SqlParameter paramPetName = new()
        {
          ParameterName = "@p_PetName", SqlDbType = SqlDbType.NVarChar, Size = 50, Direction = ParameterDirection.Output
        };

        command.Parameters.Add(paramPetName);

        command.ExecuteNonQuery();

        petName = (string) command.Parameters["@p_PetName"].Value;
      }

      CloseConnection();

      return petName;
    }

    public void ProcessCreditRisk(bool throwEx, int customerId)
    {
      OpenConnection();

      string fName, lName;

      SqlCommand cmdSelect = new("select * from Customer where Id = @p_CustomerId", _sqlConnection);

      SqlParameter paramId = new()
      {
        ParameterName = "@p_CustomerId", SqlDbType = SqlDbType.Int, Value = customerId,
        Direction = ParameterDirection.Input
      };

      cmdSelect.Parameters.Add(paramId);

      using (var dataReader = cmdSelect.ExecuteReader())
      {
        if (dataReader.HasRows)
        {
          dataReader.Read();
          fName = (string) dataReader["FirstName"];
          lName = (string) dataReader["LastName"];
        }
        else
        {
          CloseConnection();
          return;
        }
      }

      cmdSelect.Parameters.Clear();

      SqlCommand cmdUpdate = new("update Customer set LastName = LastName + ' (CreditRisk) ' where Id = @p_CustomerId",
        _sqlConnection);

      cmdUpdate.Parameters.Add(paramId);

      SqlCommand cmdInsert =
        new("insert into CreditRisk (CustomerId, FirstName, LastName) Values(@p_CustomerId, @p_FirstName, @p_LastName)",
          _sqlConnection);

      SqlParameter paramId2 = new()
      {
        ParameterName = "@p_CustomerId", SqlDbType = SqlDbType.Int, Value = customerId,
        Direction = ParameterDirection.Input
      };

      SqlParameter paramFirstName = new()
      {
        ParameterName = "@p_FirstName", Value = fName, SqlDbType = SqlDbType.NVarChar, Size = 50,
        Direction = ParameterDirection.Input
      };

      SqlParameter paramLastName = new()
      {
        ParameterName = "@p_LastName", Value = lName, SqlDbType = SqlDbType.NVarChar, Size = 50,
        Direction = ParameterDirection.Input
      };

      cmdInsert.Parameters.Add(paramId2);
      cmdInsert.Parameters.Add(paramFirstName);
      cmdInsert.Parameters.Add(paramLastName);

      SqlTransaction tx = null;

      try
      {
        tx = _sqlConnection.BeginTransaction();

        cmdInsert.Transaction = tx;
        cmdUpdate.Transaction = tx;

        cmdInsert.ExecuteNonQuery();
        cmdUpdate.ExecuteNonQuery();

        if (throwEx)
        {
          throw new Exception("Sorry! Database error! Tx failed...");
        }

        tx.Commit();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        tx?.Rollback();
      }
      finally
      {
        CloseConnection();
      }
    }
  }
}
