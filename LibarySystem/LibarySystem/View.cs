using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem
{
    class View
    {
        public bool Login(string t_username, string t_password) {
            //Call connect to DB

            //Check if username and pass which where provides are correct

            //close DB

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
