using DBConnection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR_Controller
{
    public class Context
    {
        #region Variable Declaration
        static SQL _sql = new SQL();
        #endregion

        #region Accessors

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetDatable(string tableName)
        {
            return _sql.GetDataTable(ConnectionString, tableName);
        }

        public static DataTable GetDataTable(string sqlQuery, string tableName)
        {
            return _sql.GetDataTable(ConnectionString, sqlQuery, tableName);
        }

        public static DataTable GetDataTable(string sqlQuery, string tableName, bool isReadOnly)
        {
            return _sql.GetDataTable(ConnectionString, sqlQuery, tableName, isReadOnly);
        }

        public static DataTable GetDataTable(object sqlQuery, string v)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Mutators
        public static void saveDatabaseTable (DataTable dataTable)
        {
            _sql.SaveDatabaseTable(ConnectionString, dataTable);
        }

        public static int InsertParentRecod(string tableName, string columnNames, string columnValues)
        {
            return _sql.InsertParentRecord(ConnectionString, tableName, columnNames, columnValues);
        }
        public static void DeleteRecores(string tableName, string PKName, string PKID)
        {
            _sql.DeleteRecord(ConnectionString, tableName, PKName, PKID);
        }

        public static DataTable GetDataTable(string tableName)
        {
            return _sql.GetDataTable(ConnectionString, tableName);
        }

        public static object InsertParentTable(string tableName, string columnNames, string columnValues)
        {
            return _sql.InsertParentRecord(ConnectionString, tableName, columnNames, columnValues);

        }


        #endregion
    }
}

