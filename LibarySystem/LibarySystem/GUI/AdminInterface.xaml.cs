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
            List<objVReservation> allReservations = con.Get_AllReservations();

            if (0 != allReservations.Count)
            {
                lvReservations.ItemsSource = allReservations;
            } else
            {
                lvReservations.ItemsSource = null;
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
    }
}
