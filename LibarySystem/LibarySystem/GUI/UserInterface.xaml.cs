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
            Update_Lists();
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
            Controller con= new Controller();

            //Call the search book function
            List<objBook> bookList = con.Get_Book(searchString);

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
            Controller con = new Controller();
            objBook selectBook = (objBook)lvFoundBooks.Items[lvFoundBooks.SelectedIndex];

            if (!con.Get_ReservationState(selectBook.ISBN))
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
            con.Set_Reseravtion(selectBook.ISBN, User);

        }

        private void BtnShowAll_Click(object sender, RoutedEventArgs e)
        {
            Controller con = new Controller();
            List<objBook> allBooks = con.Get_Book("All");
            //Listview 
            lvFoundBooks.ItemsSource = allBooks;
        }

        private void Update_Lists()
        {
            Controller con = new Controller();

            //Get reservations and rentals for the user
            List<objVReservation> objVReservations = con.Get_Reservations(User);
            List<objVRent> objVRents = con.Get_Rent(User);
            lvReservation.ItemsSource = objVReservations;
            lvRentals.ItemsSource = objVRents;
        }

        private void BntUpdateList_Click(object sender, RoutedEventArgs e)
        {
            Update_Lists();
        }
    }
}
