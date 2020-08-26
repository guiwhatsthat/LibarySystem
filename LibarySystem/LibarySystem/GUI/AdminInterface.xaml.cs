﻿using System;
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

            //Search all current rent rquest an fill tehm  into the Listview
            Refresh_List();
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

            //Update books
            List<objBook> allBooks = con.Get_Book("all");
            if (0 != allBooks.Count)
            {
                lvbooks.ItemsSource = allBooks;
            }
            else
            {
                lvbooks.ItemsSource = null;
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

            if (con.Approve_Reservation(selectedReservation.ISBN, User))
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
            MessageBox.Show("Söt e mahnig uslöse, aber muess mä ja nid mache i dem projekt ;)", "zu fuu", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            //Create book
            objBook newBook = new objBook(txtName.Text, txtISBN.Text, txtAuthor.Text, txtPublisher.Text,0);

            //call book creation
            if (con.Add_Book(newBook))
            {
                MessageBox.Show("Book was successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                MessageBox.Show("Couldn't create book. Please contact support", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
