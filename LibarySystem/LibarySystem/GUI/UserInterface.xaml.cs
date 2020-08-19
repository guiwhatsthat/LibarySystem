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
        public UserInterface()
        {
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

        private void BtnRent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
