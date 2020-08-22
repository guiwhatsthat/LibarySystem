using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Windows;

namespace LibarySystem
{
    class View
    {
        public bool Login(string t_username, string t_password) {
            //Create controller object 
            Controller con = new Controller();

            //Check if username and pass which where provides are correct
            bool returnValue = false;
            try
            {
                objCustomer objCustomer = con.Get_Customer(t_username);
                if (objCustomer.Password == t_password)
                {
                    returnValue = true;
                }
            } catch { }
            

            return returnValue;
        }

        public List<objBook> Get_Book(string t_Searchstring)
        {
            //Create controller object 
            Controller con = new Controller();

            //search
            var books =con.Get_Book(t_Searchstring);

            return books;
        }

        public bool Get_ReservationState(string t_ISBN)
        {
            //Create controller object 
            Controller con = new Controller();

            return con.Get_ReservationState(t_ISBN);
        }

        public void Set_Reseravtion(string t_ISBN, string t_CurrentUser)
        {
            //Create controller object 
            Controller con = new Controller();
            if (con.Set_Reseravtion(t_ISBN, t_CurrentUser)) {
                MessageBox.Show("Reservation was successfull", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                MessageBox.Show("Something went wrong." + Environment.NewLine + " Please contact support!" , "Success", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
