using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models;
using AppBookStore.Models.Domain;
using AppBookStore.Models.DTO;
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

        public IQueryable<Category> ListQueryable()
        {
            return dbContext.Categories.AsQueryable();
        }

        public CategoryListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new CategoryListVm();
            var list = dbContext.Categories.ToList();

            if(!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(x => x.Name!.ToLower().StartsWith(term)).ToList();
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

            data.CategoryList = list.AsQueryable();
            return data;
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