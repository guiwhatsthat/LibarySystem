using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//zusätzlicher Namespace
using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace LibarySystem
{
    class Controller
    {
        //Create DB Connection
        public DataContext Create_DBConnection()
        {
            //Example string "Server=DESKTOP-35P8P5I\\SQLEXPRESS;Database=OOP_DB;Connection timeout=30;Integrated Security=True"
            DataContext dbConnection = new DataContext("Server=DESKTOP-35P8P5I\\SQLEXPRESS;Database=Librarysystem;Connection timeout=30;Integrated Security=True");
            return dbConnection;
        }

        //Get user by username
        public List<Customer> Get_Customer(string t_username) {
            DataContext dbConnection = Create_DBConnection();
            Table<Customer.db_Customer> userTable = dbConnection.GetTable<Customer.db_Customer>();


            var returnList = new List<Customer>(); 

            //select
            var returnValue =
                           from i_u in userTable
                           where i_u.Username == t_username
                           select i_u;
            //Convert DB search to objects
            foreach (var i in returnValue) {
                var customer = new Customer(i.Username, i.Password, i.Surname, i.Last_name, i.Address, i.ZIP, i.City);
                returnList.Add(customer);
            }

            //Close DB connection
            dbConnection.Dispose();

            return returnList;
        }

        //Get books by Name -> Change it later so you can search with whatever you want (more than one constructor or how can I do that?)
        public List<Book> Get_Book(string t_name)
        {
            DataContext dbConnection = Create_DBConnection();
            Table<Book.db_Book> bookTable = dbConnection.GetTable<Book.db_Book>();


            var returnList = new List<Book>();

            //select
            var returnValue =
                           from i_u in bookTable
                           where i_u.Name == t_name
                           select i_u;
            //Convert DB search to objects
            foreach (var i in returnValue)
            {
                var book = new Book(i.Name, i.ISBN, i.Author, i.Publisher);
                returnList.Add(book);
            }

            //Close DB connection
            dbConnection.Dispose();

            return returnList;
        }
    }
}
