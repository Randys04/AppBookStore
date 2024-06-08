using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models;
using AppBookStore.Models.Domain;
using AppBookStore.Repositories.Abstract;

namespace AppBookStore.Repositories.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext dbContext;

        public CategoryService(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Category> List()
        {
            return dbContext.Categories.AsQueryable();
        }
    }
}