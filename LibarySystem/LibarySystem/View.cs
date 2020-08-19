using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;


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
    }
}
