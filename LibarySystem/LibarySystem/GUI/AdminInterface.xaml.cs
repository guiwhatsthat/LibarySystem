using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibarySystem.GUI
{
    /// <summary>
    /// Interaction logic for AdminInterface.xaml
    /// </summary>
    public partial class AdminInterface : Window
    {
        string User;
        public AdminInterface(string t_User)
        {
            User = t_User;
            InitializeComponent();
            Set_Permissions();

            //Search all current rent rquest an fill tehm  into the Listview
            Refresh_List();
        }

        private void Set_Permissions()
        {
            var con = new Controller();
            objUser user = con.Get_User(User);
            con.Set_AdminPermission(user, this);
        }

        private void Refresh_List()
        {
            var con = new Controller();

            //Update reservations 
            List<objVReservation> allReservations = con.Get_AllReservations();

            if (0 != allReservations.Count)
            {
                lvReservations.ItemsSource = allReservations;
            } else
            {
                lvReservations.ItemsSource = null;
            }

            //Update rent
            List<objVRent> allRents = con.Get_AllRents();
            if (0 != allRents.Count)
            {
                lvRents.ItemsSource = allRents;
            }
            else
            {
                lvRents.ItemsSource = null;
            }

            //Update books and remove book listview
            List<objBook> allBooks = con.Get_Book("all");
            if (0 != allBooks.Count)
            {
                lvbooks.ItemsSource = allBooks;
                lvRemoveBooks.ItemsSource = allBooks;
            }
            else
            {
                lvbooks.ItemsSource = null;
                lvRemoveBooks.ItemsSource = null;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                Refresh_List();
            }
        }

        private void BtnApprov_Click(object sender, RoutedEventArgs e)
        {
            Controller con = new Controller();

            if (lvReservations.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a reservation", "Please select a reservation for approval", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            objVReservation selectedReservation = (objVReservation)lvReservations.Items[lvReservations.SelectedIndex];

            if (con.Approve_Reservation(selectedReservation.ISBN, selectedReservation.Username))
            {
                MessageBox.Show("Reservation successfull", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                MessageBox.Show("Reservation failed. Please contact support", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Refresh_List();
        }

        private void BntCancel_Click(object sender, RoutedEventArgs e)
        {
            Controller con = new Controller();

            if (lvReservations.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a reservation", "Please select a reservation for approval", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            objVReservation selectedReservation = (objVReservation)lvReservations.Items[lvReservations.SelectedIndex];

            //aske user if he/she is sure to cancel
            var reservationChoice = MessageBox.Show("Do you want to cancel the reservation for the book " + selectedReservation.Name, "Are your sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (reservationChoice.ToString() == "No")
            {
                return;
            }

            if (con.Cancel_Reservation(selectedReservation.ISBN))
            {
                MessageBox.Show("Canceled successfull", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Cancellation failed. Please contact support", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Refresh_List();
        }

        private void BtnEnd_Click(object sender, RoutedEventArgs e)
        {
            Controller con = new Controller();

            if (lvRents.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a reservation", "Please select a reservation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            objVRent selectedRent = (objVRent)lvRents.Items[lvRents.SelectedIndex];

            //aske user if he/she is sure to cancel
            var reservationChoice = MessageBox.Show("Do you want to end the rental of " + selectedRent.Name + "?", "Are your sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (reservationChoice.ToString() == "No")
            {
                return;
            }

            if (con.End_Rent(selectedRent.ISBN,selectedRent.Lend_date,selectedRent.Username))
            {
                MessageBox.Show("Finished rental successfull", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Rental end failed. Please contact support", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Refresh_List();
        }

        private void BtnReminder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("A reminder was sent to the user", "reminder sent", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            lvbooks.ItemsSource = bookList;
        
        }

        private void BtnRental_Click(object sender, RoutedEventArgs e)
        {
            Controller con = new Controller();

            if (lvbooks.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a book", "Please select a book", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            objBook book = (objBook)lvbooks.Items[lvbooks.SelectedIndex];

            //Check if book is avaiable
            if(!con.Get_ReservationState(book.ISBN))
            {
                MessageBox.Show("Book is not avaiable", "Book is not avaiable", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            //aske user if he/she is sure
            var reservationChoice = MessageBox.Show("Do you want to end the rental of " + book.Name + "?", "Are your sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (reservationChoice.ToString() == "No")
            {
                return;
            }

            //search for user
            FindUserInterface findUser = new FindUserInterface();

            findUser.ShowDialog();

            if (findUser.username == null) {
                return;
            }

            //create rent entry in db
            if (con.Approve_Reservation(book.ISBN,findUser.username))
            {
                MessageBox.Show("Finished rental successfull", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                MessageBox.Show("Rental end failed. Please contact support", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddBook_Click(object sender, RoutedEventArgs e)
        {
            Controller con = new Controller();

            //check empty fields
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtISBN.Text) || string.IsNullOrEmpty(txtAuthor.Text) || string.IsNullOrEmpty(txtPublisher.Text)) {
                MessageBox.Show("Please fill in all informations", "Empty fields", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Check ISBN format
            string pattern = @"[0-9]*[-| ][0-9]*[-| ][0-9]*[-| ][0-9]*[-| ][0-9]*";
            Regex rg = new Regex(pattern);
            if (!rg.IsMatch(txtISBN.Text)) {
                MessageBox.Show("ISBN is not corretly formatted", "Wrong format", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Check if book already exists
            if (con.Get_Book(txtISBN.Text).Count == 1)
            {
                MessageBox.Show("Book already exists", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int Amount;
            if (!int.TryParse(txtAmount.Text,out Amount))
            {
                MessageBox.Show("Amount is not a number", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Create book
            objBook newBook = new objBook(txtName.Text.Trim(), txtISBN.Text.Trim(), txtAuthor.Text.Trim(), txtPublisher.Text.Trim(), 0,Amount,true);
            
            //call book creation
            if (con.Add_Book(newBook))
            {
                MessageBox.Show("Book was successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                MessageBox.Show("Couldn't create book. Please contact support", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            Controller con = new Controller();

            if (lvRemoveBooks.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a book", "Please select a book", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            objBook book = (objBook)lvRemoveBooks.Items[lvRemoveBooks.SelectedIndex];

            //aske user if he/she is sure
            var reservationChoice = MessageBox.Show("Do you want to delete " + book.Name + "?", "Are your sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (reservationChoice.ToString() == "No")
            {
                return;
            }

            if (con.Remove_Book(book))
            {
                MessageBox.Show("Book was successfully removed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Couldn't remove book. Please contact support", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            Controller con = new Controller();
            List<objReport> Report = con.Genertae_Report();
            lvReport.ItemsSource = Report;

        }

        private void BtnExportReport_Click(object sender, RoutedEventArgs e)
        {
            if (lvReport.Items.Count == 0)
            {
                MessageBox.Show("Please generate first the report", "No genereated report found", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Controller con = new Controller();
            List<objReport> Report = new List<objReport>();
            foreach (objReport item in lvReport.Items)
            {
                Report.Add(item);
            }
            string path = "c:\\Temp\\report.xlsx";
            if (con.Export_Report("c:\\Temp\\report.xlsx", Report))
            {
                MessageBox.Show("Report exported to " + path, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                MessageBox.Show("File " + path + " is in use. Please close it first", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }
    }
}
