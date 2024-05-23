using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppBookStore.Models.Domain
{
    public class BookCategory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; } // Ancla
        public int CategoryId { get; set; }
        public Category? Category { get; set; } // Ancla
    }
}