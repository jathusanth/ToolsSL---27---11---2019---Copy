using DBConnection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR_Controller
{
   public class Initializer
    {

        #region Variable Declaration

        private static string _ConnectionString = string.Empty;
        static SQL _sql = new SQL();
        private static IEnumerable<object> brands;


        
        #endregion

        #region Accessors



        #endregion

        #region Mutators

        /// < summary>
        /// this method will create the database on the specified SQL server
        /// </summary>
        public static void CreateDatabase()

        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            // call the SQL create database to build the database in the SQL server
            _sql.CreateDatabase(_ConnectionString);
            CreateDatabaseTables();
           // SeedDatabaseTables();

        }
        /// <summary>
        /// This method will create database table required in SQL server
        /// </summary>
        public static void CreateDatabaseTables()
        {
            //tool table schema
            string ToolSchema = "ToolId int IDENTITY(1,1) PRIMARY KEY, " +
                                "ToolName VARCHAR(70), " +
                                "BrandId VARCHAR(70), " +
                                 "Active BIT NOT NULL DEFAULT 0";
            // Client tabe schema
            string ClientsSechma = "ClientId int IDENTITY(1,1) PRIMARY KEY, " +
                                     "ClientName VARCHAR(70), " +
                                     "Number VARCHAR(20)";
            // Hire table schema
            string HireSchema = "HireId int IDENTITY(1,1) PRIMARY KEY, " +
                                  "ClientId int NOT NULL, " +
                                  "DateRented DATETIME NOT NULL, " +
                                  "DateReturned DATETIME NULL";
            // Hire Item table schema
            string HireItemSchema = "HireItemId int IDENTITY(1,1) PRIMARY KEY, " +
                                     "HireId int NOT NULL, " +
                                     "ToolId int NOT NULL";
            // Brand table shema
            String BrandSchema = "BrandId int IDENTITY(1,1) PRIMARY KEY, " +
                                  "BrandName VARCHAR(70)";


            // Create the tool table 
            if (!_sql.IsDatabaseTableExists(_ConnectionString, "Tool"))
            {
                _sql.CreateDatabaseTable(_ConnectionString, "Tool", ToolSchema);
                SeedTool();
            }
            // Create the client table
            if (!_sql.IsDatabaseTableExists(_ConnectionString, "Clients"))
            {
                _sql.CreateDatabaseTable(_ConnectionString, "Clients", ClientsSechma);
                SeedClients();
            }
            // Create the hire table
            if (!_sql.IsDatabaseTableExists(_ConnectionString, "Hire"))
            {
                _sql.CreateDatabaseTable(_ConnectionString, "Hire", HireSchema);
                SeedHires();
            }

            // Create the hire Item table 
            if (!_sql.IsDatabaseTableExists(_ConnectionString, "HireItem"))
            {
                _sql.CreateDatabaseTable(_ConnectionString, "HireItem", HireItemSchema);
                SeedHireItems();
            }

            // Create the Brand table 
            if (!_sql.IsDatabaseTableExists(_ConnectionString, "Brand"))
            {
                _sql.CreateDatabaseTable(_ConnectionString, "Brand", BrandSchema);
                SeedBrand();
            }
        }

        /// <summary>
        /// This method will later the schema of the specified tables
        /// </summary>

        public static void AlterDatabaseTables(string TableName, string TableSchema)
        {
            // To alter the schema of database, call this method by passing the 
            // table name and the table schema that you want to change 
            //E.g. If you want to add anew colum in Movie table called Genre, do the 
            // following:
            //create a schema for the new colum e.g.
            // string newSchema = "ADD COLUMN Genre VARCHAR(20) NULL"
            // The call this method by:
            //AlterDatabaseTables("Movie", newSchema);

            _sql.AlterDatabaseTable(_ConnectionString, TableName, TableSchema);
        }

        /// <summary>
        /// This method will populate table with dummy data
        /// </summary>
        private static void SeedDatabaseTables()
        {
            //If(_SQL.IsDatabaseTableExit()
            SeedTool();
            SeedClients();
            SeedHires();
            SeedHireItems();
            SeedBrand();
        }
        /// <summary>
        /// this method will seed the brand dummy data
        /// </summary>
        private static void SeedBrand()
        {
            List<string> brands = new List<string>
            {
                "1, 'Nick'",
                "2, 'Dmk'",
                "3, 'Revolution'",
                "4, 'Max'",
            };

            //ColumnNames must match the order of the test data above
            string columnNames = "BrandId, BrandName";

            // push the date to the database table 
            foreach (var brand in brands)
            {
                _sql.InsertRecord(_ConnectionString, "Brand", columnNames, brand);
            }
        }

        /// <summary>
        /// This method will seed the tool with dummy data
        /// </summary>
        private static void SeedTool()
        {
            //create a list of Tool data
            List<string> tool = new List<string>
            {
                "1, 'Rake',1, 1",
                "2, 'Hammer',2, 1",
                "3, 'File',3, 1",
                "4, 'Saw',4, 1",
                "5, 'Screwdriver',3, 1",
                "6, 'Phillips',1, 1",
                "7, 'Chisel',3, 1",
                "8, 'Brace',2, 1",
                "9, 'Hatchet',2, 1",
                "10, 'Hacksaw',3, 1",
            };

            //ColumnNames must match the order of the test data above
            string columnNames = "ToolId, ToolName, BrandId, Active";

            // push the date to the database table 
            foreach (var Tool in tool)
            {
                _sql.InsertRecord(_ConnectionString, "Tool", columnNames, Tool );
            }
        }

        /// <summary>
        /// This method will send seed to client with dummy data
        /// </summary>
        private static void SeedClients()
        {
            // create the list of customer
            List<string> clients = new List<string>
            {
                "1, 'Abdulkalam', '0415 1245'",
                "2, 'Raja Raja Cholan', '1452 2456'",
                "3, 'Jathusanth','2450 1342'",
                "4, 'Vijay', '4578 35522'",
                "5, 'Mani', '4578 35511'",
                "6, 'Bawah', '4578 35533'",
                "7, 'Doni', '4578 35545'",
                "8, 'Virath Koli', '4578 35145'",
                "9, 'Sanjay', '4578 31202'",
                "10, 'Mahes', '4578 37802'",               
            };

            //ColumnName must match the order of the test data above 
            string columnNames = "ClientId, ClientName, Number";
            
            // push the date to the database table 
            foreach (var client in clients)
            {
                _sql.InsertRecord(_ConnectionString, "Clients", columnNames, client);
            }
        }

        /// <summary>
        /// this methos will send seed to Hires with dummy data
        /// </summary>
        private static void SeedHires()
        {
            /// create the list of Rental
            List<string> hires = new List<string>
            {
                $"1,1,'2017/01/01',null",
                $"2,2,'2018/08/21',null",
                $"3,1,'2019/08/21',null",

            };

            // ColumnName must match th eorder of the test data above 
            string columnName = "HireId, ClientId, DateRented,DateReturned";
            // push the data to database table         
            foreach (var hire in hires)
            {
                _sql.InsertRecord(_ConnectionString, "Hire", columnName, hire);
            }
        }

        /// <summary>
        /// this methos will send seed to Hire Item with dummy data
        /// </summary>
        private static void SeedHireItems()
        {

            List<string> HireItems = new List<string>()
            {
                "1,1,1",
                "2,1,2",
                "3,2,3",
                "4,3,1",
                "5,3,1",
                "6,3,3",

            };
            // columnNames must match the order of the test data above
            string columnNames = "HireItemId, HireId, ToolId";

            foreach (var HireItem in HireItems)
            {
                _sql.InsertRecord(_ConnectionString, "HireItem", columnNames, HireItem);
            }
        }
        #endregion
    }
}
