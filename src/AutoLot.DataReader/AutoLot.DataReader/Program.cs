﻿using System;
using Microsoft.Data.SqlClient;

namespace AutoLot.DataReader
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("***** Fun with Data Readers *****\n");

// Create and open a connection.
      using (SqlConnection connection = new SqlConnection())
      {
        connection.ConnectionString =
          "Data Source=(localdb)\\mssqllocaldb;Integrated Security=true; Initial Catalog=AutoLot";
        connection.Open();

        // Create a SQL command object.
        string sql =
          @"Select i.id, m.Name as Make, i.Color, i.Petname
          FROM Inventory i
          INNER JOIN Make m on m.Id = i.MakeId";
        SqlCommand myCommand = new SqlCommand(sql, connection);

        // Obtain a data reader a la ExecuteReader().
        using (SqlDataReader myDataReader = myCommand.ExecuteReader())
        {
          // Loop over the results.
          while (myDataReader.Read())
          {
            Console.WriteLine($"-> Make: {myDataReader["Make"]}, PetName: {myDataReader ["PetName"]}, Color: {myDataReader["Color"]}.");
          }
        }
      }
      Console.ReadLine();
    }
  }
}
