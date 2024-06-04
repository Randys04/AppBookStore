using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models.Domain;
using AppBookStore.Models.DTO;

namespace AppBookStore.Repositories.Abstract
{
    public interface IBookService
    {
        bool Add(Book book);
        bool Update(Book book);
        Book GetById(int id);
        bool Delete(int id);
        BookListVm List(string term="", bool paging = false, int currentPage = 0);
        List<int> GetCategoriesByBook(int bookId);
    }
}