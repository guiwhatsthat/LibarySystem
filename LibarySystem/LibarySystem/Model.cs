using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace LibarySystem
{


    class Customer
    {
        //SQL mapping
        [Table(Name = "Customer")]
        public class db_Customer
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey_1
            {
                get;
                set;
            }

            //Mapper auf Feld Name der Gruppe
            [Column]
            public string Username;
            [Column]
            public string Password
            {
                get;
                set;
            }
            [Column]
            public string Surname;
            [Column]
            public string Last_name;
            [Column]
            public string Address;
            [Column]
            public int ZIP;
            [Column]
            public string City;
        }
        public Customer(string t_Username, string t_Password, string t_Surname, string t_Last_name, string t_adress, int t_zip, string t_city)
        {

        }
    }

    class Book
    {
        //SQL mapping
        [Table(Name = "Book")]
        public class db_Book
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey_1
            {
                get;
                set;
            }

            //Mapper auf Feld Name der Gruppe
            [Column]
            public string Name;
            [Column]
            public string ISBN;
            [Column]
            public string Author;
            [Column]
            public string Publisher;
        }
    }

    class Reservation
    {
        //SQL mapping
        [Table(Name = "Reservation")]
        public class db_Reservation
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey_1
            {
                get;
                set;
            }

            //Mapper auf Feld Name der Gruppe
            [Column]
            public DateTime Reservation_date;
            [Column]
            public bool Done;
            [Column]
            public int FKey_Book;
            [Column]
            public int FKey_Customer;
        }
    }

    class objReservation
    {
        public DateTime Reservation_date { get; }
        public bool Done { get; }
        public int FKey_Book { get; }
        public int FKey_Customer { get; }

        public objReservation(DateTime t_Reservation_date, bool t_Done, int t_FKey_Book, int t_FKey_Customer)
        {
            Reservation_date = t_Reservation_date;
            Done = t_Done;
            FKey_Book = t_FKey_Book;
            FKey_Customer = t_FKey_Customer;
        }
    }

    class objBook
    {
        public string Name { get; }
        public string ISBN { get; }
        public string Author { get; }
        public string Publisher { get; }
        public int PK { get; }

        public objBook(string t_Name, string t_ISBN, string t_Author, string t_Publisher, int t_PK)
        {
            Name = t_Name;
            ISBN = t_ISBN;
            Author = t_Author;
            Publisher = t_Publisher;
            PK = t_PK;
        }
    }

    class objCustomer
    {
        public string Username { get; }
        public string Password { get; }
        public string Surename { get; }
        public string Lastname { get; }
        public string Adress { get; }
        public int Zip { get; }
        public string City { get; }
        public int PK { get; }

        public objCustomer(string t_Username, string t_Password, string t_Surname, string t_Last_name, string t_adress, int t_zip, string t_city, int t_PK)
        {
            Username = t_Username;
            Password = t_Password;
            Surename = t_Surname;
            Lastname = t_Last_name;
            Adress = t_adress;
            Zip = t_zip;
            Adress = t_city;
            PK = t_PK;
        }
    }

}
