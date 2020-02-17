using DBConnection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VR_Controller
{
    public static class Initializer
    {
        #region Variable Declaration

        private static string _ConnectionString = string.Empty;
        static SQL _sql = new SQL();
        private static IEnumerable<object> rentals;
        private static IEnumerable<object> customers;


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
            SeeDatabaseTables();

        }
        /// <summary>
        /// This method will create database table required in SQL server
        /// </summary>
        public static void CreateDatabaseTables()
        {
            //movie table schema
            string MoviesSchema = "MovieId int IDENTITY(1,1) PRIMARY KEY, " +
                                "MovieName VARCHAR(70)";
            // Customer tabe schema
            string CustomersSechma = "CustomerId int IDENTITY(1,1) PRIMARY KEY, " +
                                     "CustomerName VARCHAR(70), " +
                                     "phone VARCHAR(20)";
            // Rental table schema
            string RentalSchema = "RentalId int IDENTITY(1,1) PRIMARY KEY, " +
                                  "CustomerId int NOT NULL, " +
                                  "DateRented DATETIME NOT NULL, " +
                                  "DateReturned DATETIME NULL";
            // Rental Item table schema
            string RentalItemSchema = "RentalItemId int IDENTITY(1,1) PRIMARY KEY, " +
                                     "RentalId int NOT NULL, " +
                                     "MovieId int NOT NULL";

            // Create the movie table 
            if(!_sql.IsDatabaseTableExists(_ConnectionString, "Movie"))
            {
                _sql.CreateDatabaseTable(_ConnectionString, "Movie", MoviesSchema);
            }
            // Create the custome table
            if(!_sql.IsDatabaseTableExists(_ConnectionString, "Customers"))
            {
                _sql.CreateDatabaseTable(_ConnectionString, "Customers", CustomersSechma);
            }
            // Create the Rental table
            if (!_sql.IsDatabaseTableExists(_ConnectionString, "Rental"))
            {
                _sql.CreateDatabaseTable(_ConnectionString, "Rental", RentalSchema);
            }

            // Create the Rental Item table 
            if (!_sql.IsDatabaseTableExists(_ConnectionString, "RentalItem"))
            {
                _sql.CreateDatabaseTable(_ConnectionString, "RentalItem", RentalItemSchema);
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
        private static void SeeDatabaseTables()
        {
            SeedMovies();
            SeedCustomers();
            SeedRentals();
            SeedRentalItems();
        }

    

    

        /// <summary>
        /// This method will seed the movies with dummy data
        /// </summary>
        private static void SeedMovies()
        {
            //create a list of movie data
            List<string> movies = new List<string>
            {
                "1,'The Avengers' ",
                "2, 'Star Wars'",
                "3, 'The Matrix'",
            };

            //ColumnNames must match the order of the test data above
            string columnNames = "MovieId, MovieName";

            // push the date to the database table 
            foreach(var movie in movies)
            {
                _sql.InsertRecord(_ConnectionString, "Movie", columnNames, movie);
            }


        }

        /// <summary>
        /// This method will send seed to customer with dummy data
        /// </summary>
        private static void SeedCustomers()
        {
            // create the list of customer
            List<string> customers = new List<string>
            {
                "1, 'Abdulkalam', '0415 1245'",
                "2, 'Raja Raja Cholan', '1452 2456'",
                "3, 'Jathusanth','2450 1342'",
                "4, 'Vijay', '4578 35522'",

            };

            //ColumnName must match the order of the test data above 
            string columnNames = "CustomerId, CustomerName, Phone";

           
            // push the date to the database table 
            foreach (var customer in customers)
            {                
                _sql.InsertRecord(_ConnectionString, "Customers", columnNames, customer);
            }
        }

        /// <summary>
        /// this methos will send seed to Rental with dummy data
        /// </summary>
        private static void SeedRentals()
        {
            /// create the list of Rental
            List<string> rentals =new List<string>
            {
                $"1,1,'2017/01/01',null",
                $"2,2,'2018/08/21',null",
                $"3,1,'2019/08/21',null",

            };

            // ColumnName must match th eorder of the test data above 
            string columnName = "RentalId, CustomerId, DateRented,DateReturned";
             // push the data to database table         
            foreach(var rental in rentals)
            {
                _sql.InsertRecord(_ConnectionString, "Rental", columnName, rental);
            }
        }

        /// <summary>
        /// this methos will send seed to Rental Item with dummy data
        /// </summary>
        private static void SeedRentalItems()
        {

            List<string> RentalItems = new List<string>()
            {
                "1,1,1",
                "2,1,2",
                "3,2,3",
                "4,3,1",
                "5,3,1",
                "6,3,3",

            };
            // columnNames must match the order of the test data above
            string columnNames = "RentalItemId, RentalId, MovieId";

            foreach (var rentalItem in RentalItems)
            {
                _sql.InsertRecord(_ConnectionString, "RentalItem", columnNames, rentalItem);
            }
        }
        #endregion
    }
}
