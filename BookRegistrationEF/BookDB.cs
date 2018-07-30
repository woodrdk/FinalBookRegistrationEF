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

        public static void Delete(Book b)
        {
            var context = new BookContext();
            context.Book.Add(b);
            // mark the book as deleted
            context.Entry(b).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public static void Delete( String isbn)
        {   // connnected scenario where the dbcontext trackes entities in memory for deletion 
            var context = new BookContext();
            Book bookToDelete = context.Book.Find(isbn); // pull book from DB to make EF track for deletion
            context.Book.Remove(bookToDelete);           // mark book as deleted
            context.SaveChanges();                       // save changes
        }
    }
}
