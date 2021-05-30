using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace AutoLot.Dal.BulkImport
{
  public static class ProcessBulkImport
  {
    private const string ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Integrated Security=true; Initial Catalog=AutoLot";
    private static SqlConnection _sqlConnection = null;

    private static void OpenConnection()
    {
      _sqlConnection = new SqlConnection {ConnectionString = ConnectionString};
      _sqlConnection.Open();
    }

    private static void CloseConnection()
    {
      if (_sqlConnection?.State != ConnectionState.Closed)
      {
        _sqlConnection?.Close();
      }
    }

    public static void ExecuteBulkImport<T>(IEnumerable<T> records, string tableName)
    {
      OpenConnection();
      using SqlConnection conn = _sqlConnection;

      SqlBulkCopy bc = new(conn) {DestinationTableName = tableName};

      IMyDataReader<T> dataReader = new MyDataReader<T>(records.ToList(), _sqlConnection, "dbo", tableName);

      try
      {
        bc.WriteToServer(dataReader);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      finally
      {
        CloseConnection();
      }
    }
  }
}
