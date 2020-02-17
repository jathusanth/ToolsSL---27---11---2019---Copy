using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection
{
    interface IAlterDatabase
    {
        void SaveDatabaseTable(string connectionString, DataTable dataTable);
        void CreateDatabase(string connectionString);
        void CreateDatabaseTable(string connectionString,
                                 string tableName, string tableStructure);
        int InsertRecord(string connectionString,
                         string tableName, string columnNames,
                         string columnValues);
        void AlterDatabaseTable(string connectionString, string tableName, string tableStructure);
        int InsertParentRecord(string connectionString, string tableName, string columnNames, string columnValues);
        bool UpdateRecord(string connectionString, string tableName, string columnNamesAndValues, string criteria);
        void DeleteRecord(string connectionString, string tableName, string PKName, string PKID);
    }
}
