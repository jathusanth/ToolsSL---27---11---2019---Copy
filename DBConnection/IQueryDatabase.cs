using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection
{
    interface IQueryDatabase
    {
        DataTable GetDataTable(string connectionString, string tableNam);
        DataTable GetDataTable(string connectionString, string tableName, bool isReadOnly);
        DataTable GetDataTable(string connectionString, string sqlQuery, string tableName);
        DataTable GetDataTable(string connectionString, string sqlQuery, string tableName, bool isReadOnly);
    }
}
