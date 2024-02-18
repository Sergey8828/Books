using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using Formatting = Newtonsoft.Json.Formatting;

namespace Project_Books
{
    /// <summary>
    ///Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> books = new ObservableCollection<Book>();
        public ObservableCollection<Book> matchingBooks = new ObservableCollection<Book>();

        string[] categories = { "Harry Potter", "Don Kihot", "Lord of the rings", "Sherlock Holms" };

        string[] genres = { "Adventure", "Horror", "Detective", "Novel" };
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Random randomFactory = new Random();

            ////create 5 book objects

            Book b1 = new Book("Harry Potter", new DateTime(2018, 1, 15), 50m, "Adventure");
            Book b2 = GetRandomBook(randomFactory);
            Book b3 = GetRandomBook(randomFactory);
            Book b4 = GetRandomBook(randomFactory);
            Book b5 = GetRandomBook(randomFactory);

            //add to collection
            books.Add(b1);
            books.Add(b2);
            books.Add(b3);
            books.Add(b4);
            books.Add(b5);

            ////display on screen
            lbxBooks.ItemsSource = books;

            decimal total = Book.PriceTotal;
            tblkTotal.Text = string.Format("{0:C}", total);

            //populate combo box
            Array.Sort(categories);
            cbxFilter.ItemsSource = categories;
        }

        //generate random book
        private Book GetRandomBook(Random randomFactory)
        {
            Random rf = new Random();

            int randNumber = randomFactory.Next(0, 3);

            string randomTitle = categories[randNumber];

            DateTime randomDate = DateTime.Now.AddDays(-randomFactory.Next(0, 32));

            decimal randomAmount = (decimal)randomFactory.Next(50, 400);

            string randomGenre = genres[randNumber];


            Book randomBook = new Book(randomTitle, randomDate, randomAmount, randomGenre);

            return randomBook;

        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //identify which expense is selected
            Book selectedBook = lbxBooks.SelectedItem as Book;

            if (selectedBook != null)
            {
                //remove that book
                Book.PriceTotal -= selectedBook.Price;
                books.Remove(selectedBook);

                decimal total = Book.PriceTotal;
                tblkTotal.Text = string.Format("{0:C}", total);
            }
        }

        //save expense objects to JSON
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //get string of objects - json formatted
            string json = JsonConvert.SerializeObject(books, Formatting.Indented);

            //write that to file

            using (StreamWriter sw = new StreamWriter(@"c:\temp\books.json"))
            {
                sw.Write(json);
            }
            MessageBox.Show("You saved all books into the file");
        }


        //loads json file
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //connect to a file
            using (StreamReader sr = new StreamReader(@"c:\temp\books.json"))
            {
                //read text
                string json = sr.ReadToEnd();

                //convert from json to objects
                books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(json);

                //refresh the display
                lbxBooks.ItemsSource = books;
            }
            MessageBox.Show("Books are loaded from the file");
        }

        //shows all expenses, used after a search to see everything, remove filters
        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            lbxBooks.ItemsSource = books;

        }
        //search for book by category
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //read info from screen
            string searchTerm = tbxSearch.Text.ToLower();

            if (!String.IsNullOrEmpty(searchTerm))
            {
                //clear expenses to that blank at start of every search
                matchingBooks.Clear();

                //search collection of expenses for matches

                foreach (Book book in books)
                {
                    string bookType = book.Genre;

                    if (bookType.Equals(searchTerm))
                    {
                        matchingBooks.Add(book);
                    }
                }
                //display matches on screen
                lbxBooks.ItemsSource = matchingBooks;
            }
        }

        private void btnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            //Random randomFactory = new Random();
            //Book book = GetRandomBook(randomFactory);
            //books.Add(book);

            //int total = Book.BooksTotal;
            //tblkTotal.Text = string.Format("{0}", total);

            WindowAddBook addBook = new WindowAddBook();
            addBook.Owner = this;
            addBook.ShowDialog();
        }

        //filter by type of book
        private void btnSearch_Click_1(object sender, RoutedEventArgs e)
        {
            //determine what the user selected
            string selectedBookGenre = tbxSearch.Text;

            if (!String.IsNullOrEmpty(selectedBookGenre))
            {
                //clear search results
                matchingBooks.Clear();
               
                //search in the collection
                foreach (Book book in books)
                {
                    //find match
                    string bookGenre = book.Genre;

                    if (bookGenre.Equals(selectedBookGenre))
                    {
                        //add match to search results
                        matchingBooks.Add(book);
                    }                 
                }

                //update display
                lbxBooks.ItemsSource = matchingBooks;
            }
        }

        private void cbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //determine what user has selected 
            string selectedBookType = cbxFilter.SelectedItem as string;

            if (selectedBookType != null)
            {
                //clear search results
                matchingBooks.Clear();

                //search in the collection
                foreach (Book book in books)
                {
                    //find match
                    string bookTitle = book.Title;

                    if (book.Title.Equals(selectedBookType))
                    {
                        //add match to search results
                        matchingBooks.Add(book);
                    }
                }
                //update display
                lbxBooks.ItemsSource = matchingBooks;
            }
        }      
        
 //update the total price
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            decimal updatedPrice = 0;

            foreach (var item in lbxBooks.Items)
            {
                decimal price =((Book)item).Price;
                updatedPrice += price;
            }
            tblkTotal.Text = string.Format("{0:C}", updatedPrice);

        }
    }
}



