﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.BulkImport
{
  public interface IMyDataReader<T> : IDataReader
  {
    List<T> Records { get; set; }
  }
}
