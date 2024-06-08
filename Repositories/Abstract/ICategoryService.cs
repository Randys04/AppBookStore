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
    }
}