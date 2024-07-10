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
using AppBookStore.Models.Domain;

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

        [HttpPost]
        public IActionResult Add(Category category)
        {       
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(category);
                }

                var result =_categoryService.Add(category);
                if(result)
                {
                    TempData["msg"] = "Category saved successfully";
                    return RedirectToAction(nameof(Add));
                }
                
                TempData["msg"] = "Error saving the category";
                return RedirectToAction(nameof(Add));
            }
            catch (System.Exception)
            {
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            var categoryEdit = _categoryService.GetById(id);       
            return View(categoryEdit);
        }

        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);       
            return RedirectToAction(nameof(CategoryList));
        }

        public IActionResult CategoryList()
        {
            var categoriesList = _categoryService.List();       
            return View(categoriesList);
        }

    }
}