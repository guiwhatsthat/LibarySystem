﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//zusätzlicher Namespace
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Common;
using System.IO;

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
        public objUser Get_User(string t_username) {
            try
            {
                DataContext dbConnection = Create_DBConnection();
                Table<User.db_User> userTable = dbConnection.GetTable<User.db_User>();


                var returnList = new List<objUser>();

                //select
                var returnValue =
                               from i_u in userTable
                               where i_u.Username == t_username
                               select i_u;
                //Convert DB search to objects
                foreach (var i in returnValue) {
                    var customer = new objUser(i.Username, i.Password, i.Surname, i.Last_name, i.Adress, i.ZIP, i.City, i.Pkey_1, i.Write, i.Write_rent);
                    returnList.Add(customer);
                }

                //Close DB connection
                dbConnection.Dispose();

                objUser userReturn = returnList.First();

                return userReturn;

            }
            catch
            {
                return null;
            }
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
            } else if (t_name.ToLower() == "all")
            {
                returnedBooks =
                           from i_u in bookTable
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
                var book = new objBook(i.Name, i.ISBN, i.Author, i.Publisher, i.Pkey_1, i.Amount,i.Avaiable);
                returnList.Add(book);
            }

            //Close DB connection
            dbConnection.Dispose();

            return returnList;
        }

        //Check if a book is currently avaiable or not
        public bool Get_ReservationState(string t_ISBN)
        {
            //Get DB connction
            var dbConnection = Create_DBConnection();
            objBook book = Get_Book(t_ISBN).First();

            return book.Avaiable;
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
                objUser user = Get_User(t_CurrentUser);

                //Get Book for reservation
                objBook book = Get_Book(t_ISBN).First();



                //Sql insert
                // Create a new Order object.
                Reservation.db_Reservation reservation = new Reservation.db_Reservation
                {
                    Reservation_date = DateTime.Now,
                    Done = false,
                    FKey_Book = book.PK,
                    FKey_User = user.PK
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

        public List<objVReservation> Get_AllReservations()
        {
            //Get DB connction
            var dbConnection = Create_DBConnection();

            Table<Reservation.V_Reservation> reservationView = dbConnection.GetTable<Reservation.V_Reservation>();

            var reservationList = new List<objVReservation>();

            //select
            var currentReservations =
                           from i_u in reservationView
                           select i_u;
            //Convert DB search to objects
            foreach (var i in currentReservations)
            {
                var reservation = new objVReservation(i.Name, i.ISBN, i.Reservation_date, i.Username, i.Last_name, i.Surname);
                reservationList.Add(reservation);
            }

            return reservationList;
        }

        public List<objVReservation> Get_Reservations(string t_Username)
        {
            //Get DB connction
            var dbConnection = Create_DBConnection();

            Table<Reservation.V_Reservation> reservationView = dbConnection.GetTable<Reservation.V_Reservation>();

            var reservationList = new List<objVReservation>();

            //select
            var currentReservations =
                           from i_u in reservationView
                           where i_u.Username == t_Username
                           select i_u;
            //Convert DB search to objects
            foreach (var i in currentReservations)
            {
                var reservation = new objVReservation(i.Name, i.ISBN, i.Reservation_date, i.Username, i.Last_name, i.Surname);
                reservationList.Add(reservation);
            }

            return reservationList;
        }

        public List<objVRent> Get_AllRents()
        {
            //Get DB connction
            var dbConnection = Create_DBConnection();

            Table<Rent.V_Rents> rentView = dbConnection.GetTable<Rent.V_Rents>();

            var rentList = new List<objVRent>();

            //select
            var allRents =
                           from i_u in rentView
                           where i_u.Return_date == null
                           select i_u;
            //Convert DB search to objects
            foreach (var i in allRents)
            {
                var rent = new objVRent(i.Name, i.ISBN, i.Lend_date, i.End_rentdate, i.Username, i.Surname, i.Last_name);
                rentList.Add(rent);
            }

            return rentList;
        }

        public List<objVRent> Get_Rent(string t_Username)
        {
            //Get DB connction
            var dbConnection = Create_DBConnection();

            Table<Rent.V_Rents> rentView = dbConnection.GetTable<Rent.V_Rents>();

            var rentList = new List<objVRent>();

            //select
            var allRents =
                           from i_u in rentView
                           where i_u.Return_date == null && i_u.Username == t_Username
                           select i_u;
            //Convert DB search to objects
            foreach (var i in allRents)
            {
                var rent = new objVRent(i.Name, i.ISBN, i.Lend_date, i.End_rentdate, i.Username, i.Surname, i.Last_name);
                rentList.Add(rent);
            }

            return rentList;
        }

        public bool Set_DoneFlag(string t_ISBN, bool t_value)
        {
            //Get DB connction
            var dbConnection = Create_DBConnection();
            Table<Reservation.db_Reservation> reservationTable = dbConnection.GetTable<Reservation.db_Reservation>();
            objBook book = Get_Book(t_ISBN).First();
            var reservationList = new List<objReservation>();

            //select
            var currentReservations =
                           from i_u in reservationTable
                           where i_u.FKey_Book == book.PK
                           select i_u;
            foreach (var i in currentReservations)
            {
                i.Done = t_value;
            }
            dbConnection.SubmitChanges();
            return true;
        }

        public bool Approve_Reservation(string t_ISBN, string t_Username)
        {
            objBook book = Get_Book(t_ISBN).First();
            objUser user = Get_User(t_Username);

            try {
                var dbConnection = Create_DBConnection();
                DateTime today = DateTime.Now;
                DateTime returnDate = today.AddDays(30);
                Rent.db_Rent rent = new Rent.db_Rent
                {
                    Lend_date = today,
                    End_rentdate = returnDate,
                    FKey_Book = book.PK,
                    FKey_User = user.PK

                };

                // Add the new object to the Orders collection.
                Table<Rent.db_Rent> rentTable = dbConnection.GetTable<Rent.db_Rent>();
                rentTable.InsertOnSubmit(rent);
                dbConnection.SubmitChanges();

                //Set done flag on Reservation
                Set_DoneFlag(t_ISBN, true);
            } catch
            {
                return false;
            }

            return true;
        }

        public bool Cancel_Reservation(string t_ISBN)
        {
            try
            {
                Set_DoneFlag(t_ISBN, true);
            } catch
            {
                return false;
            }
            return true;
        }

        public bool End_Rent(string t_ISBN, DateTime t_Reservation_date, string t_username)
        {
            try
            {
                DataContext dbConnection = Create_DBConnection();
                Table<Rent.db_Rent> rentTable = dbConnection.GetTable<Rent.db_Rent>();
                objUser user = Get_User(t_username);
                objBook book = Get_Book(t_ISBN).First();

                //select
                var returnValue =
                               from i in rentTable
                               where i.FKey_User == user.PK && i.FKey_Book == book.PK && i.Lend_date == t_Reservation_date
                               select i;
                //Convert DB search to objects
                foreach (var i in returnValue)
                {
                    i.Return_date = DateTime.Now;
                }
                dbConnection.SubmitChanges();
            } catch
            {
                return false;
            }
            return true;
        }

        public bool Add_Book(objBook t_book)
        {
            bool returnValue = true;
            try
            {
                //Get DB connction
                var dbConnection = Create_DBConnection();

                Book.db_Book newBook = new Book.db_Book
                {
                    Name = t_book.Name,
                    ISBN = t_book.ISBN,
                    Author = t_book.Author,
                    Publisher = t_book.Publisher,
                    Amount = t_book.Amount,
                    Avaiable = true
                };

                //Sql insert
                Table<Book.db_Book> reservationTable = dbConnection.GetTable<Book.db_Book>();
                reservationTable.InsertOnSubmit(newBook);
                dbConnection.SubmitChanges();


            }
            catch
            {
                returnValue = false;
            }

            return returnValue;
        }

        public bool Remove_Book(objBook t_book)
        {
            try
            {
                DataContext dbConnection = Create_DBConnection();
                Table<Book.db_Book> bookTable = dbConnection.GetTable<Book.db_Book>();

                //Convert DB search to objects
                Book.db_Book removeBook = new Book.db_Book
                {
                    Pkey_1 = t_book.PK,
                    Name = t_book.Name,
                    ISBN = t_book.ISBN,
                    Author = t_book.Author,
                    Publisher = t_book.Publisher
                };
                bookTable.Attach(removeBook);
                bookTable.DeleteOnSubmit(removeBook);
                dbConnection.SubmitChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Set_AdminPermission(objUser t_User, GUI.AdminInterface t_IntAdmin)
        {

            if (t_User.Write == true) {
                t_IntAdmin.tbAddBook.IsEnabled = true;
                t_IntAdmin.tbRemoveBook.IsEnabled = true;
            }

            if (t_User.Write_rent == true)
            {
                t_IntAdmin.tbApprovel.IsEnabled = true;
                t_IntAdmin.tbCurrentRent.IsEnabled = true;
                t_IntAdmin.tbTriggerRental.IsEnabled = true;
            }

            if (t_User.Write == false && t_User.Write_rent == false) {
                t_IntAdmin.tbReport.Focus();
            }
        }

        // Simplifyes the code in the view
        public bool Login(string t_username, string t_password)
        {
            //Check if username and pass which where provides are correct
            bool returnValue = false;

            objUser objUser = Get_User(t_username);
            if (objUser.Password == t_password)
            {
                returnValue = true;
            }

            return returnValue;
        }

        public List<objReport> Genertae_Report()
        {
            var dbConnection = Create_DBConnection();
            dbConnection.Connection.Open();

            var cmd = dbConnection.Connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.SP_TOPBOOKS";
            var param = cmd.CreateParameter();
            param.DbType = DbType.Int16;
            param.Direction = ParameterDirection.Input;
            param.ParameterName = "@TOPBOOKSAMOUNT";
            param.Value = 10;

            cmd.Parameters.Add(param);

            List<objReport> reportEntries = new List<objReport>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader["Name"].ToString();
                    int count;
                    Int32.TryParse(reader["Amount_Rent"].ToString(), out count);
                    objReport entry = new objReport(name, count);
                    reportEntries.Add(entry);
                }
            }
            return reportEntries;

        }

        public bool Export_Report(string t_path, List<objReport> t_Report)
        {
            // Create path first and remove file if exists
            try
            {
                var folder = System.IO.Path.GetDirectoryName(t_path);
                if (File.Exists(t_path))
                {
                    File.Delete(t_path);
                }

                if (!Directory.Exists(folder))
                {
                    System.IO.File.Create(folder);
                }
            }
            catch (IOException e)
            {
                return false;
            }

            //Verweise auf die verwendete EXCEL-Struktur
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            Microsoft.Office.Interop.Excel.Range oRng;

            object misvalue = System.Reflection.Missing.Value;
            //Start Excel and get Application object.
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;

            //Get a new workbook.
            oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

            //Uberschriften setzen.
            oSheet.Cells[1, 1] = "Name";
            oSheet.Cells[1, 2] = "Amount rent";

            // HIER DURCH DATAGRID LOOPEN UND ABFUELLEN
            int counter = 1;
            foreach (objReport item in t_Report)
            {
                counter++;
                oSheet.Cells[counter, 1] = item.Name;
                oSheet.Cells[counter, 2] = item.Amount_Rent;

            }

            //Format A1:D1 zentrieren und fett
            oSheet.get_Range("A1", "B1").Font.Bold = true;

            oXL.Visible = false;
            oXL.UserControl = false;
            oWB.SaveAs(t_path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            oWB.Close();

            return true;
            
        }
    }
}
