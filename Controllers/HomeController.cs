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
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;

        public HomeController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var books = _bookService.List(term, true, currentPage);
            return View(books);
        }

        public IActionResult BookDetails(int bookId)
        {
            Book book = _bookService.GetById(bookId);
            return View(book);
        }

        public IActionResult About()
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