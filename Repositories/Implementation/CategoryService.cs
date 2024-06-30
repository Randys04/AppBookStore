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

        public bool Add(Category category)
        {
            try
            {
                dbContext.Add(category);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var categoryToDelete = GetById(id);
                dbContext.Remove(categoryToDelete);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Category GetById(int id)
        {
            return dbContext.Categories.Find(id)!;
        }

        public IQueryable<Category> List()
        {
            return dbContext.Categories.AsQueryable();
        }

        public bool Update(Category category)
        {
            try
            {
                dbContext.Update(category);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}