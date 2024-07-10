using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models.Domain;

namespace AppBookStore.Models.DTO
{
    public class CategoryListVm
    {
        public IQueryable<Category>? CategoryList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}