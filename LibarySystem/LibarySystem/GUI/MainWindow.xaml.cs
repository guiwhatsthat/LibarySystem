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

        private void BntLogin_Click(object sender, RoutedEventArgs e)
        {
            //Create view object
            View objView = new View();

            //Call login function
            bool loginCorrect = objView.Login("DummyStringOne", "DummyStringTwo");
            
            //Create user interface
            if (loginCorrect)
            {
                UserInterface guiUser = new UserInterface();
                guiUser.Show();
            }
        }
    }
}
