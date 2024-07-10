using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models.DTO;
using AppBookStore.Models.Domain;

namespace AppBookStore.Repositories.Abstract
{
    public interface ICategoryService
    {
        IQueryable<Category> ListQueryable();
        CategoryListVm List(string term = "", bool paging = false, int currentPage = 0);
        bool Add(Category category);
        bool Update(Category category);
        Category GetById(int id);
        bool Delete(int id);
    }
}