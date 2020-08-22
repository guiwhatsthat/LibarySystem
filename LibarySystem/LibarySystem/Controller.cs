using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//zusätzlicher Namespace
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text.RegularExpressions;

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
        public objCustomer Get_Customer(string t_username) {
            DataContext dbConnection = Create_DBConnection();
            Table<Customer.db_Customer> userTable = dbConnection.GetTable<Customer.db_Customer>();


            var returnList = new List<objCustomer>(); 

            //select
            var returnValue =
                           from i_u in userTable
                           where i_u.Username == t_username
                           select i_u;
            //Convert DB search to objects
            foreach (var i in returnValue) {
                var customer = new objCustomer(i.Username, i.Password, i.Surname, i.Last_name, i.Address, i.ZIP, i.City, i.Pkey_1);
                returnList.Add(customer);
            }

            //Close DB connection
            dbConnection.Dispose();
            objCustomer customerReturn = returnList.First();
            return customerReturn;
        }

        //Get books by Name -> Change it later so you can search with whatever you want (more than one constructor or how can I do that?)
        public List<objBook> Get_Book(string t_name)
        {
            DataContext dbConnection = Create_DBConnection();
            Table<Book.db_Book> bookTable = dbConnection.GetTable<Book.db_Book>();

            //return value
            var returnList = new List<objBook>();

            //definiere nach was man suchen muss
            var returnedBooks = Enumerable.Empty<Book.db_Book>().AsQueryable();
            //checken ob es eine ISBN Nummer ist
            string pattern = @"[0-9]*[-| ][0-9]*[-| ][0-9]*[-| ][0-9]*[-| ][0-9]*";
            Regex rg = new Regex(pattern);
            if (rg.IsMatch(t_name))
            {
                //search for ISBN
                returnedBooks =
                           from i_u in bookTable
                           where i_u.ISBN == t_name
                           select i_u;
            }
            else {
                //search for author, titel, publisher
                returnedBooks =
                           from i_u in bookTable
                           where i_u.Name == t_name || i_u.Author == t_name || i_u.Publisher == t_name
                           select i_u;
            }

            //Convert DB search to objects
            foreach (var i in returnedBooks)
            {
                var book = new objBook(i.Name, i.ISBN, i.Author, i.Publisher, i.Pkey_1);
                returnList.Add(book);
            }

            //Close DB connection
            dbConnection.Dispose();

            return returnList;
        }

        //Check if a book is currently avaiable or not
        public bool Get_ReservationState (string t_ISBN)
        {
            bool returnValue = false;
            //Get DB connction
            var dbConnection = Create_DBConnection();
            objBook book = Get_Book(t_ISBN).First();
            Table<Reservation.db_Reservation> reservationTable = dbConnection.GetTable<Reservation.db_Reservation>();

            var reservationList = new List<objReservation>();

            //select
            var currentReservations =
                           from i_u in reservationTable
                           where i_u.FKey_Book == book.PK
                           select i_u;
            //Convert DB search to objects
            foreach (var i in currentReservations)
            {
                var reservvation = new objReservation(i.Reservation_date, i.Done, i.FKey_Book, i.FKey_Customer);
                reservationList.Add(reservvation);
            }

            if (reservationList.Count > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        //Create a reservation
        public bool Set_Reseravtion(string t_ISBN, string t_CurrentUser)
        {
            bool returnValue = true;
            try
            {
                //Get DB connction
                var dbConnection = Create_DBConnection();

                //Get_CurrentUser
                objCustomer user = Get_Customer(t_CurrentUser);

                //Get Book for reservation
                objBook book = Get_Book(t_ISBN).First();



                //Sql insert
                // Create a new Order object.
                Reservation.db_Reservation reservation = new Reservation.db_Reservation
                {
                    Reservation_date = DateTime.Now,
                    Done = false,
                    FKey_Book = book.PK,
                    FKey_Customer = user.PK
                };

                // Add the new object to the Orders collection.
                Table<Reservation.db_Reservation> reservationTable = dbConnection.GetTable<Reservation.db_Reservation>();
                reservationTable.InsertOnSubmit(reservation);
                dbConnection.SubmitChanges();

              
            }
            catch
            {
                returnValue = false;
            }

            return returnValue;
        }

    }
}
