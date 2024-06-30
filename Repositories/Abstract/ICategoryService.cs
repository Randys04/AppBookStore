using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models.Domain;

namespace AppBookStore.Repositories.Abstract
{
    public interface ICategoryService
    {
        IQueryable<Category> List();
        bool Add(Category category);
        bool Update(Category category);
        Category GetById(int id);
        bool Delete(int id);
    }
}