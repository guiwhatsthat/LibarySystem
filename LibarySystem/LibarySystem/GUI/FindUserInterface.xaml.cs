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
    /// Interaction logic for FindUserInterface.xaml
    /// </summary>
    public partial class FindUserInterface : Window
    {
        public string username;
        public FindUserInterface()
        {
            InitializeComponent();
        }

        private void BtnSearchUser_Click(object sender, RoutedEventArgs e)
        {
            string searchString = txtUsername.Text;
            if (string.IsNullOrEmpty(searchString))
            {
                MessageBox.Show("Search can not be empty", "Search can not be empty", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Controller con = new Controller();
            objUser foundUser = con.Get_User(searchString);

            if (foundUser == null)
            {
                MessageBox.Show("Can not find a user with the username " + searchString, "Search can not be empty", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            username = foundUser.Username;
            this.Close();
        }
    }
}
