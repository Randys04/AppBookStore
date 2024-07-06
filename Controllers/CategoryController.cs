using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using AppBookStore.Repositories.Implementation;
using AppBookStore.Repositories.Abstract;

namespace AppBookStore.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Add()
        {       
            return View();
        }

        public IActionResult Edit(int id)
        {
            var categoryEdit = _categoryService.GetById(id);       
            return View(categoryEdit);
        }

        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);       
            return RedirectToAction(nameof(BookList));
        }

        public IActionResult BookList()
        {
            var categoriesList = _categoryService.List();       
            return View(categoriesList);
        }

    }
}