using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Books
{
    public class Book
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }

        public static decimal PriceTotal { get; set; }

        public Book(string title,  DateTime date, decimal amt, string genre)
        {
            Title = title;
            Date = date;
            Genre = genre;
            Price = amt;

            PriceTotal += amt;
        }
        public Book()
        {

        }
        public Book(string title, DateTime date, decimal price)
        {
            Title = title;
            Date = date;
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2} Price {3:C}", Title, Date.ToShortDateString(), Genre, Price);
        }
    }
}

