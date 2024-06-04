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
            return false;
        }

        public bool Delete(Book book)
        {
            return false;
        }

        public Book GetById(int id)
        {
            return null!;
        }

        public List<int> GetCategoriesByBook(int bookId)
        {
            return null!;
        }

        public BookListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            return null!;
        }

        public bool Update(Book book)
        {
            return false;
        }
    }
}