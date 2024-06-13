using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models.Domain;
using AppBookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppBookStore.Controllers
{
    public class BookController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;
        private readonly IBookService _bookService;

        public BookController(ICategoryService categoryService, IFileService fileService, IBookService bookService)
        {
            _categoryService = categoryService;
            _fileService = fileService;
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult Add(Book book)
        {
            book.CategoriesList = _categoryService.List()
                .Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem {Text = a.Name, Value = a.Id.ToString()});
                
            if(!ModelState.IsValid)
            {
                return View(book);
            }

            if(book.ImageFile != null)
            {
                var result =_fileService.SaveImage(book.ImageFile);
                if(result.Item1 == 0)
                {
                    TempData["msg"] = result.Item2;
                    return View(book);
                }

                var imageName = result.Item2;
                book.CoverImage = imageName;
            }

            var bookResult = _bookService.Add(book);
            if(bookResult)
            {
                TempData["msg"] = "Book saved successfully";
                return RedirectToAction(nameof(Add));
            }

            TempData["msg"] = "Error saving the book";
            return View(book);
        }

        public IActionResult Add()
        {
            var book = new Book();
            book.CategoriesList = _categoryService.List()
                .Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem {Text = a.Name, Value = a.Id.ToString()});
                
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return RedirectToAction(nameof(BooksList));
        }

        public IActionResult BooksList()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}