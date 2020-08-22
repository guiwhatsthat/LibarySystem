using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibarySystem
{
    /// <summary>
    /// Interaction logic for UserInterface.xaml
    /// </summary>
    public partial class UserInterface : Window
    {
        string User;
        public UserInterface(string t_User)
        {
            User = t_User;
            InitializeComponent();
        }

        private void BtnSearchBook_Click(object sender, RoutedEventArgs e)
        {
            string searchString = txtBookSearch.Text;
            if (string.IsNullOrEmpty(searchString))
            {
                MessageBox.Show("The search can not be empty", "Please enter a search string", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            //Create view object
            View objView = new View();

            //Call the search book function
            List<objBook> bookList = objView.Get_Book(searchString);

            //Listview 
            lvFoundBooks.ItemsSource = bookList;
        }

        //Ganze logik von hier muss in das view !!!!!!!!!!!!!!!!
        private void BtnRent_Click(object sender, RoutedEventArgs e)
        {
            if (lvFoundBooks.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a book", "Please enter a search string", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Pop Up reservation question
            //first I should check if the book is currently avaiable 
            //call function check reservation
            //Create view object
            View objView = new View();
            objBook selectBook = (objBook)lvFoundBooks.Items[lvFoundBooks.SelectedIndex];

            if (objView.Get_ReservationState(selectBook.ISBN))
            {
                MessageBox.Show("Book is currently not avaiable", "Sorry", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //ask for approvle
            var reservationChoice = MessageBox.Show("Do you want to make a reservation for the book " + selectBook.Name + " from " + selectBook.Author, "Are your sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (reservationChoice.ToString() == "No")
            {
                return;
            }

            //Do reservation sql boooooooooooooy
            objView.Set_Reseravtion(selectBook.ISBN, User);

        }
    }
}
