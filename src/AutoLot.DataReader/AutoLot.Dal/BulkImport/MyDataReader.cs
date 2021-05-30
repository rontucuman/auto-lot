﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using AutoLot.Dal.Models;
using Microsoft.Data.SqlClient;

namespace AutoLot.Dal.BulkImport
{
  public class MyDataReader<T> : IMyDataReader<T>
  {
    private readonly SqlConnection _connection;
    private readonly string _schema;
    private readonly string _tableName;
    private int _currentIndex = -1;
    private readonly PropertyInfo[] _propertyInfos;
    private readonly Dictionary<int, string> _nameDictionary;

    public List<T> Records { get; set; }

    public MyDataReader(List<T> records, SqlConnection connection, string schema, string tableName)
    {
      _connection = connection;
      _schema = schema;
      _tableName = tableName;
      Records = records;
      _propertyInfos = typeof(T).GetProperties();
      _nameDictionary = new Dictionary<int, string>();

      DataTable schemaTable = GetSchemaTable();

      for (int x = 0; x < schemaTable?.Rows.Count; x++)
      {
        DataRow col = schemaTable.Rows[x];
        string columnName = col.Field<string>("ColumnName");

        _nameDictionary.Add(x, columnName);
      }
    }

    public bool GetBoolean(int i)
    {
      throw new NotImplementedException();
    }

    public byte GetByte(int i)
    {
      throw new NotImplementedException();
    }

    public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
    {
      throw new NotImplementedException();
    }

    public char GetChar(int i)
    {
      throw new NotImplementedException();
    }

    public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
    {
      throw new NotImplementedException();
    }

    public IDataReader GetData(int i)
    {
      throw new NotImplementedException();
    }

    public string GetDataTypeName(int i)
    {
      throw new NotImplementedException();
    }

    public DateTime GetDateTime(int i)
    {
      throw new NotImplementedException();
    }

    public decimal GetDecimal(int i)
    {
      throw new NotImplementedException();
    }

    public double GetDouble(int i)
    {
      throw new NotImplementedException();
    }

    public Type GetFieldType(int i)
    {
      throw new NotImplementedException();
    }

    public float GetFloat(int i)
    {
      throw new NotImplementedException();
    }

    public Guid GetGuid(int i)
    {
      throw new NotImplementedException();
    }

    public short GetInt16(int i)
    {
      throw new NotImplementedException();
    }

    public int GetInt32(int i)
    {
      throw new NotImplementedException();
    }

    public long GetInt64(int i)
    {
      throw new NotImplementedException();
    }

    public string GetName(int i)
    {
      throw new NotImplementedException();
    }

    public int GetOrdinal(string name)
    {
      throw new NotImplementedException();
    }

    public string GetString(int i)
    {
      throw new NotImplementedException();
    }

    public object GetValue(int i) => _propertyInfos
      .First(x => x.Name.Equals(_nameDictionary[i], StringComparison.OrdinalIgnoreCase))
      .GetValue(Records[_currentIndex]);

    public int GetValues(object[] values)
    {
      throw new NotImplementedException();
    }

    public bool IsDBNull(int i)
    {
      throw new NotImplementedException();
    }

    public int FieldCount => _propertyInfos.Length;

    public object this[int i] => throw new NotImplementedException();

    public object this[string name] => throw new NotImplementedException();

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public void Close()
    {
      throw new NotImplementedException();
    }

    public DataTable? GetSchemaTable()
    {
      using SqlCommand schemaCommand = new($"select * from {_schema}.{_tableName}", _connection);

      using SqlDataReader reader = schemaCommand.ExecuteReader(CommandBehavior.SchemaOnly);

      return reader.GetSchemaTable();
    }

    public bool NextResult()
    {
      throw new NotImplementedException();
    }

    public bool Read()
    {
      if (_currentIndex + 1 >= Records.Count)
      {
        return false;
      }

      _currentIndex++;

      return true;
    }

    public int Depth { get; }
    public bool IsClosed { get; }
    public int RecordsAffected { get; }
  }
}