using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppBookStore.Models.Domain
{
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public virtual ICollection<Book>? BookRelationList { get; set; }
        public virtual ICollection<BookCategory>? BookCatagoryRelationList { get; set; }
    }
}