using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProviderFactory
{
  enum DataProviderEnum
  {
  //OleDb is Windows only and is not supported in .NET Core
  SqlServer,
#if PC
    OleDb,
#endif
    Odbc
  }
}
