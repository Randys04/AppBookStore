using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models;
using AppBookStore.Models.Domain;
using AppBookStore.Models.DTO;
using AppBookStore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AppBookStore.Repositories.Implementation
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext dbContext;
        public BookService(DatabaseContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public bool Add(Book book)
        {
            try
            {
                dbContext.Books.Add(book);
                dbContext.SaveChanges();
                foreach(int categoryId in book.Categories!)
                {
                    var bookCategory = new BookCategory 
                    {
                        BookId = book.Id,
                        CategoryId = categoryId
                    };
                    dbContext.BooksCategories.Add(bookCategory);
                }
                dbContext.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Book bookToDelete = GetById(id); 
                if (bookToDelete == null)
                {
                    return false;
                }
                var bookCategories = dbContext.BooksCategories.Where(x => x.BookId == bookToDelete.Id);
                dbContext.BooksCategories.RemoveRange(bookCategories);
                dbContext.Books.Remove(bookToDelete);
                dbContext.SaveChanges();
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
            
        }

        public Book GetById(int id)
        {
            return dbContext.Books.Find(id)!;
        }

        public List<int> GetCategoriesByBook(int bookId)
        {
            return dbContext.BooksCategories.Where(x => x.BookId == bookId).Select(x => x.CategoryId).ToList();   
        }

        public BookListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            return null!;
        }

        public bool Update(Book book)
        {
            try
            {
                var categoriesToRemove = dbContext.BooksCategories.Where(x => x.BookId == book.Id);
                foreach (var category in categoriesToRemove) // Elimina las categorias asociadas al libro
                {
                    dbContext.BooksCategories.Remove(category);
                }
                
                foreach(var categoryId in book.Categories!) // Agrega las nuevas categorias
                {
                    var bookCategory = new BookCategory
                    {
                        BookId = book.Id,
                        CategoryId = categoryId
                    };
                    dbContext.BooksCategories.Add(bookCategory);
                }

                dbContext.Books.Update(book); // Acctualiza el libro
                dbContext.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
            
        }
    }
}