using LibraryAutomation.Data;
using LibraryAutomation.Data.Enums;
using LibraryAutomation.Data.Models;
using LibraryAutomation.Data.Repositories;
using LibraryAutomation.Data.Requests;
using LibraryAutomation.Data.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace LibraryAutomation.Handler
{
    public class BookHandler
    {
        private BookRepository bookRepository = new BookRepository();
        private UserRepositories userRepositories = new UserRepositories();
        public BookHandler()
        {

        }
        public void SaveBook(SaveBookRequest request, int userId)
        {

            var newBook = new Book
            {
                BookName = request.BookName,
                BookType = request.BookType,
                Category = request.Category,
                WrittenBy = request.WrittenBy,
                Subcategory = request.Subcategory,
                DateOfIssue = request.DateOfIssue,
                BookStatus = Data.Enums.BookStatus.Exist,
                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                LastUpdateBy = userId
            };
            bookRepository.SaveBook(newBook);

        }

        public BookModel GetBook(int bookId)
        {


            var book = bookRepository.GetBook(bookId);
            if (book != null)
            {
                return new BookModel
                {
                    BookName = book.BookName,
                    Category = bookRepository.GetCatagory(book.Category).CategoryName,
                    Subcategory = book.Subcategory != null ? bookRepository.GetCatagory(book.Subcategory ?? 0).CategoryName : "",
                    BookType = bookRepository.GetType(book.Category).BookTypeName,
                    WrittenBy = book.WrittenBy,
                    LastUpdateBy = book.LastUpdateBy != null ? userRepositories.GetUserById(book.LastUpdateBy ?? 0).UserName : "",
                    DateOfIssue = book.DateOfIssue,
                    UploadDate = book.CreateDate,
                    LastUpdateDate = book.LastUpdateDate,

                };
            }
            return null;
        }


        public List<BookFilterModel> GetBooksWithFilter(FilterType filterType, int id)
        {
            var bookList = new List<Book>();
            var model = new List<BookFilterModel>();


            switch (filterType)
            {
                case FilterType.ByType: bookList = bookRepository.GetBookByType(id); break;

                case FilterType.ByCatagory: bookList = bookRepository.GetBookByCatagory(id); break;

                case FilterType.BySubCatagory: bookList = bookRepository.GetBookBySubCatagory(id); break;

                default: break;

            }

            foreach (var book in bookList)
            {

                model.Add(new BookFilterModel
                {
                    Id = book.Id,
                    BookName = book.BookName

                });
            }

            return model;
        }

        public void DeleteBook(int bookId, int userId)
        {
            var book = bookRepository.GetBook(bookId);
            if (book != null)
            {
                book.BookStatus = Data.Enums.BookStatus.Deleted;
                book.LastUpdateDate = DateTime.Now;
                book.LastUpdateBy = userId;
                bookRepository.SaveBook(book);
            }
        }

        public string UpdateBook(SaveBookRequest request, int userId)
        {
            var book = bookRepository.GetBook(request.BookId ?? 0);
            if (book != null)
            {
                book.LastUpdateDate = DateTime.Now;
                book.LastUpdateBy = userId;
                book.BookName = request.BookName;
                book.BookType = request.BookType;
                book.Category = request.Category;
                book.WrittenBy = request.WrittenBy;
                book.Subcategory = request.Subcategory;
                book.DateOfIssue = request.DateOfIssue;
                book.LastUpdateDate = DateTime.Now;
                book.LastUpdateBy = userId;
                bookRepository.SaveBook(book);

                return string.Empty;
            }
            else
            {
                return request.Language == Data.Enums.Language.Eng ? "Book Not Found." : "Kitap Bulunamadı.";
            }
        }


        public List<TypeModel> GetTypeList()
        {
            var model = new List<TypeModel>();
            var typeList = bookRepository.GetTypeList();

            foreach (var type in typeList)
            {
                model.Add(new TypeModel
                {
                    Id = type.Id,
                    Name = type.BookTypeName
                });
            }

            return model;
        }

        public List<CatagoryModel> GetCatagoryList()
        {
            var model = new List<CatagoryModel>();
            var catagoryList = bookRepository.GetCatagoryList();

            foreach (var catagory in catagoryList)
            {
                model.Add(new CatagoryModel
                {
                    Id = catagory.Id,
                    Name = catagory.CategoryName,
                    Description = catagory.CategoryDescription

                });
            }
            return model;
        }


    }
}