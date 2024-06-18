using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models.Domain;
using AppBookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AppBookStore.Controllers
{
    [Authorize]
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
                
            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var editBook = _bookService.GetById(id);
            var bookCategories = _bookService.GetCategoriesByBook(id);
            var multiSelectListCategories = new MultiSelectList(_categoryService.List(), "Id", "Name", bookCategories);

            editBook.MultiCategoriesSelectList = multiSelectListCategories;
            return View(editBook);
        }

        [HttpPost]
        public IActionResult Edit(Book editBook)
        {
            var bookCategories = _bookService.GetCategoriesByBook(editBook.Id);
            var multiSelectListCategories = new MultiSelectList(_categoryService.List(), "Id", "Name", bookCategories);

            editBook.MultiCategoriesSelectList = multiSelectListCategories;

            if(!ModelState.IsValid)
            {
                return View (editBook);
            }
            
            if(editBook.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(editBook.ImageFile);
                if(fileResult.Item1 == 0)
                {
                    TempData["msg"] = "La imagen no fue guardada";
                }

                var imageName = fileResult.Item2;
                editBook.CoverImage = imageName;
            }

             var result = _bookService.Update(editBook);

            if(!result)
            {
                TempData["msg"] = "No se puedo actualizar el libro";
                return View (editBook);
            }

            TempData["msg"] = "Libro actualizado";
            return View (editBook);
        }

        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);
            return RedirectToAction(nameof(BooksList));
        }

        public IActionResult BooksList()
        {
            var books = _bookService.List();
            return View(books);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}