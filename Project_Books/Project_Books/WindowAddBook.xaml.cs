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

namespace Project_Books
{
    /// <summary>
    /// Interaction logic for WindowAddBook.xaml
    /// </summary>
    public partial class WindowAddBook : Window
    {
        public WindowAddBook()
        {
            InitializeComponent();

            cbxTitle.ItemsSource = new string[] { "Harry Potter", "Lord of the rings", "Dracula", "Sherlock Holms"};

            cbxGenre.ItemsSource = new string[] { "Adventure", "Horror", "Detective", "Novel" };
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //read data from screen
            string title = cbxTitle.SelectedItem as string;
            DateTime date = dpDate.SelectedDate.Value;
            string genre = cbxGenre.SelectedItem as string; 
            decimal price = Convert.ToDecimal(tbxPrice.Text);
            

            //create book object
            Book newbook = new Book(title, date, price, genre);

            //get reference to main window
            MainWindow main = Owner as MainWindow;

            //add that to our collection of expenses
            main.books.Add(newbook);

            //close the window
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //close the window
            this.Close();
        }
    }
}
