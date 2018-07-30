using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRegistrationEF
{
    static class BookDB
    {
        public static List<Book> GetAllBooks()
        {
            BookContext context = new BookContext();
            List<Book> allBooks =
                (from b in context.Book
                select b).ToList();
            return allBooks;
        }

        public static void Add(Book b)
        {
            BookContext context = new BookContext();
            // assume book is valid
            context.Book.Add(b);
            context.SaveChanges();
        }

        // how to update a database entry
        /* 
         * EF will track an object if you pull it out of the database and then do modifications
         */
           
        public static void Update(Book b)
        {// not the most effective as it accesses the database more than needed to do it
            BookContext context = new BookContext();
            Book originalBook = context.Book.Find(b.ISBN);
            originalBook.Price = b.Price;
            originalBook.Title = b.Title;
            context.SaveChanges();
        }

        // more effecient way to update the object
        public static void UpdateAlt(Book b)
        {
            BookContext context = new BookContext();
            // add book object to current context
            context.Book.Add(b);

            // let EF know the book already exists
            context.Entry(b).State = EntityState.Modified; // or System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
