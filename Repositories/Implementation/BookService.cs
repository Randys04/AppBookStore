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
            var data = new BookListVm();
            var list = dbContext.Books.ToList();

            if(!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(x => x.Title!.ToLower().StartsWith(term)).ToList();
            }

            if(paging) // realizar paginacion
            {
                int pageSize = 5;
                int listCount = list.Count;
                int totalPages = (int)Math.Ceiling(listCount / (double)pageSize);

                // Con skip indicamos a partir de que posicion empezamos a contar y con take indicamos cuantos records tomamos
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                data.TotalPages = totalPages;
                data.CurrentPage = currentPage;
                data.PageSize = pageSize;
            }

            foreach(var book in list)
            {
                var categories = (
                    from category in dbContext.Categories
                        join lc in dbContext.BooksCategories
                        on category.Id equals lc.CategoryId
                        where lc.BookId == book.Id
                        select category.Name
                ).ToList();
                string categoriesNames = string.Join("-", categories); // se pasa la cadena de categorias a un solo string
                book.CategoriesNames = categoriesNames;
            }
            
            data.BookList = list.AsQueryable();
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