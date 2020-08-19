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
            var objCustomer = con.Get_Customer("s.gasser"); // We should get an objecz from type Customer back, this class is defined in the model
            //close DB
            string testpw = "password";
            return true;
        }

        public List<string> Get_Book(string t_Searchstring)
        {
            //Call connect to DB

            //Call the read DB with search string

            //close DB

            //temp returnvalue
            List<string> bookList = new List<string>();
            bookList.Add("Book 1");
            bookList.Add("Book 2");
            bookList.Add("Book 3");

            //Return the list of found books
            return bookList;
        }
    }
}
