using LibraryAutomation.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Repositories
{
    public class BookRepository
    {
        public void SaveBook(Book book)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                if (book.Id == default(int))
                {
                    dbContext.Books.Add(book);

                }
                else
                {
                    dbContext.Books.Attach(book);
                    dbContext.Entry(book).Property(e => e.BookName).IsModified = true;
                    dbContext.Entry(book).Property(e => e.BookStatus).IsModified = true;
                    dbContext.Entry(book).Property(e => e.LastUpdateDate).IsModified = true;
                    dbContext.Entry(book).Property(e => e.BookType).IsModified = true;
                    dbContext.Entry(book).Property(e => e.Category).IsModified = true;
                    dbContext.Entry(book).Property(e => e.Subcategory).IsModified = true;
                    dbContext.Entry(book).Property(e => e.LastUpdateBy).IsModified = true;

                }
                dbContext.SaveChanges();
            }
        }

        public Book GetBook(int id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.Books.Where(b => b.Id == id && b.BookStatus == Enums.BookStatus.Exist).FirstOrDefault();
            }
        }

        public List<Book> GetBookByType(int typeId)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.Books.Where(b => b.BookType == typeId && b.BookStatus == Enums.BookStatus.Exist).ToList();
            }
        }

        public List<Book> GetBookByCatagory(int catagoryId)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.Books.Where(b => b.Category == catagoryId && b.BookStatus == Enums.BookStatus.Exist).ToList();
            }
        }

        public List<Book> GetBookBySubCatagory(int SubCatagoryId)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.Books.Where(b => b.Subcategory == SubCatagoryId && b.BookStatus == Enums.BookStatus.Exist).ToList();
            }
        }
        public BookType GetType(int id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.BookTypes.Where(b => b.Id == id).FirstOrDefault();
            }
        }

        public Category GetCatagory(int id)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.Categories.Where(b => b.Id == id).FirstOrDefault();
            }
        }

        public List<BookType> GetTypeList()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.BookTypes.ToList();
            }
        }

        public List<Category> GetCatagoryList()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.Categories.ToList();
            }
        }


    }
}