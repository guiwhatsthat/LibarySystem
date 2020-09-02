using LibarySystem.GUI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibarySystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                BntLogin_Click(sender,e);
            }
        }

        private void BntLogin_Click(object sender, RoutedEventArgs e)
        {
            //Create view object
            Controller con= new Controller();

            //Call login function
            bool loginCorrect = con.Login(txtUsername.Text, txtPassword.Password);
            
            //Create user interface
            if (loginCorrect)
            {
                objUser currentUser = con.Get_User(txtUsername.Text);
                if (currentUser.Write == true || currentUser.Write_rent == true) {
                    AdminInterface guiAdmin = new AdminInterface(txtUsername.Text);
                    guiAdmin.Show();
                }
                else
                {
                    UserInterface guiUser = new UserInterface(txtUsername.Text);
                    guiUser.Show();
                }
            } else
            {

                MessageBox.Show("Username or password is not correct", "Invalide credntials", MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}
