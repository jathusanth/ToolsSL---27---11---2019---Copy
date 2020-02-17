using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DBConnection
{
    public class SQL : IQueryDatabase, IAlterDatabase
    {
        #region Variable Declarations

        /// <summary>
        /// The connections string of the SQL database.
        /// </summary>
        Logger Log;

        #endregion

        #region Constructors

        public SQL()
        {
            #region nlog.config
            /// make sure to create a nlog.config in the DEBUG folder and paste the following scripts
            /*
                <?xml version="1.0" encoding="utf-8" ?>
                <nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
                    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

                    <targets>
                    <target name="logfile" xsi:type="File" fileName="logfile.txt" />
                    <target name="logconsole" xsi:type="Console" />
                    </targets>

                    <rules>
                    <logger name="*" minlevel="Debug" writeTo="logconsole" />
                    <logger name="*" minlevel="Debug" writeTo="logfile" />
                    </rules>
                </nlog>
             */
            ///
            #endregion

            LogManager.LoadConfiguration("nlog.config");
            Log = LogManager.GetCurrentClassLogger();            
        }

        #endregion

        #region Mutators

        /// <summary>
        /// This method will create a database on a specified server
        /// </summary>
        /// <param name="connectionString">The connection string of the database.</param>
        public void CreateDatabase(string connectionString)
        { 
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                if (IsDatabaseExists(conn.DataSource, conn.Database)) return;

                string serverConnectionString = $"Data Source={conn.DataSource}; " +
                                   $"Integrated Security=True";
                string sqlQuery = $"CREATE DATABASE {conn.Database}";
                    
                using (SqlConnection connServer = new SqlConnection(serverConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, connServer))
                    {
                        if (connServer.State == ConnectionState.Closed) connServer.Open();
                        command.ExecuteNonQuery();
                        connServer.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
            }
        }

        /// <summary>
        /// This method will create a database table on a specified server
        /// and database.
        /// </summary>
        /// <param name="connectionString">The connection string of the database.</param>
        /// <param name="tableName">Table Name</param>
        /// <param name="tableStructure">Table Structure</param>
        public void CreateDatabaseTable(string connectionString, string tableName, string tableStructure)
        {
            try
            {
                // this query will check if the tableName already exists, if not, Create the table, otherwise, do nothing
                string sqlQuery = $"IF NOT EXISTS (SELECT name FROM sysobjects " +
                                  $"WHERE name = '{tableName}') " +
                                  $"CREATE TABLE {tableName} ({tableStructure})";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
            }
        }

        /// <summary>
        /// This method will insert a record in the database.
        /// </summary>
        /// <param name="connectionString">The connection string of the database.</param>
        /// <param name="tableName">Table Name</param>
        /// <param name="columnNames">Column Names of the table</param>
        /// <param name="columnValues">Column Values</param>
        /// <returns>int NewId</returns>
        public int InsertRecord(string connectionString, string tableName, string columnNames, string columnValues)
        {
            int Id = 0;

            string sqlQuery = $"SET IDENTITY_INSERT {tableName} ON INSERT INTO {tableName} ({columnNames}) VALUES ({columnValues}) " +
                              $"SET IDENTITY_INSERT {tableName} OFF SELECT SCOPE_IDENTITY()";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        Id = (int)(decimal)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
            }

            return Id;
        }


        /// <summary>
        /// This method will delete a record in the database
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="tableName">Table Name</param>
        /// <param name="PKName">Primary Key Name</param>
        /// <param name="PKID">Primary Key ID</param>
        public void DeleteRecord(string connectionString, string tableName, string PKName, string PKID)
        {
            string sqlQuery = $"DELETE FROM {tableName} WHERE {PKName} = {PKID} SELECT SCOPE_IDENTITY()";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
            }
        }
        /// <summary>
        /// This method will alter the specified database table on a specified
        /// server and database
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="tableName">Table Name</param>
        /// <param name="tableStructure">Table Structure</param>
        public void AlterDatabaseTable(string connectionString, string tableName, string tableStructure)
        {
            try
            {
                string sqlQuery = $"ALTER TABLE {tableName} ({tableStructure})";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
            }
        }



        /// <summary>
        /// This method will insert a record in the database.
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="tableName">Destination Table</param>
        /// <param name="columnNames">Column Names of the table</param>
        /// <param name="columnValues">Column Values</param>
        /// <returns>int NewId</returns>
        public int InsertParentRecord(string connectionString, string tableName, string columnNames, string columnValues)
        {
            int Id = 0;

            try
            {
                string sqlQuery = $"INSERT INTO {tableName} ({columnNames}) " +
                                  $"VALUES ({columnValues}) " +
                                  $"SELECT SCOPE_IDENTITY()";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        Id = (int)(decimal)command.ExecuteScalar();
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
            }

            return Id;
        }

        /// <summary>
        /// This method will update a database table.
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="dataTable">Source Table</param>
        public void SaveDatabaseTable(string connectionString, DataTable dataTable)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {dataTable.TableName}", conn))
                    {
                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                        adapter.InsertCommand = commandBuilder.GetInsertCommand();
                        adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                        adapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                        if (conn.State == ConnectionState.Closed) conn.Open();
                        adapter.Update(dataTable);
                        conn.Close();
                        dataTable.AcceptChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Log.Error(e.ToString());
            }
        }

        /// <summary>
        /// This method will update a record in the database.
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="tableName">Table Name</param>
        /// <param name="columnNamesAndValues">Column Names and corresponding values</param>
        /// <param name="criteria">Update Criteria</param>
        /// <returns>bool IsOk</returns>
        public bool UpdateRecord(string connectionString, string tableName, string columnNamesAndValues, string criteria)
        {
            bool IsOK = false;

            string sqlQuery = $"UPDATE {tableName} SET {columnNamesAndValues} WHERE {criteria}";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteNonQuery();
                        IsOK = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                IsOK = false;
                Log.Error(e.ToString());
            }

            return IsOK;
        }


        #endregion

        #region Accessors

        /// <summary>
        /// This method will get an updateable table from the database.
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="tableName">Source Table</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string connectionString, string tableName)
        {
            DataTable Table = new DataTable
            {
                TableName = tableName
            };


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        adapter.Fill(Table);
                        conn.Close();
                        Table.PrimaryKey = new DataColumn[] { Table.Columns[0] };
                        Table.Columns[0].AutoIncrement = true;
                        // to get the current last PKID value
                        if (Table.Rows.Count > 0)
                            Table.Columns[0].AutoIncrementSeed = long.Parse(Table.Rows[Table.Rows.Count - 1][0].ToString());
                        Table.Columns[0].AutoIncrementStep = 1;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
            }

            return Table;
        }

        /// <summary>
        /// This method will get an Read-Only table from the database.
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="tableName">Source Table</param>
        /// <param name="isReadOnly">Specify if table is Read-Only</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string connectionString, string tableName, bool isReadOnly)
        {
            if (isReadOnly == false) return GetDataTable(connectionString, tableName);

            DataTable Table = new DataTable
            {
                TableName = tableName
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand($"SELECT * FROM {tableName}", conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Table.Load(reader);
                            conn.Close();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
            }

            return Table;
        }

        /// <summary>
        /// This method will get an updateable table from the database.
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="sqlQuery">SQL query to retrieve records.</param>
        /// <param name="tableName">Source Table</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string connectionString, string sqlQuery, string tableName)
        {
            DataTable Table = new DataTable
            {
                TableName = tableName
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        adapter.Fill(Table);
                        conn.Close();
                        Table.PrimaryKey = new DataColumn[] { Table.Columns[0] };
                        Table.Columns[0].AutoIncrement = true;
                        // to get the current last PKID value
                        if(Table.Rows.Count > 0)
                            Table.Columns[0].AutoIncrementSeed = long.Parse(Table.Rows[Table.Rows.Count - 1][0].ToString());
                        Table.Columns[0].AutoIncrementStep = 1;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
            }

            return Table;
        }

        /// <summary>
        /// This method will get an Read-Only table from the database.
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="sqlQuery">SQL query to retrieve records.</param>
        /// <param name="tableName">Source Table<</param>
        /// <param name="isReadOnly">Specify if table is Read-Only</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string connectionString, string sqlQuery, string tableName, bool isReadOnly)
        {
            if (isReadOnly == false) return GetDataTable(sqlQuery, tableName);

            DataTable Table = new DataTable
            {
                TableName = tableName
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Table.Load(reader);
                            conn.Close();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
            }

            return Table;
        }

        #endregion

        #region Helper Methods

        public bool IsDatabaseExists(string serverName, string databaseName)
        {
            string connectionString = $"Data source={serverName}; " +
                               $"Integrated Security = True";

            string sqlQuery = $"SELECT  database_id FROM sys.databases WHERE Name = " +
                              $"'{databaseName}'";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        object objResult = command.ExecuteScalar();
                        conn.Close();
                        return (objResult != null);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
            }

            return true;
        }

        /// <summary>
        /// This method will check if the specified database table exists in the specified database on a specified database server
        /// </summary>
        /// <param name="connectionString">The connection string of the database.</param>
        /// <param name="tableName">Table Name to check</param>
        /// <returns>bool</returns>
        public bool IsDatabaseTableExists(string connectionString, string tableName)
        {
            bool IsExists = false;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand($"SELECT 1 FROM {tableName} WHERE 1=0", conn))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        command.ExecuteScalar();
                        IsExists = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Log.Error(e.ToString());
                IsExists = false;
            }

            return IsExists;
        }

        #endregion
    }
}
