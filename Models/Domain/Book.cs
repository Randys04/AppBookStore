using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppBookStore.Models.Domain
{
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? CreateDate { get; set; }
        public string? CoverImage{ get; set;}
        [Required]       
        public string? Author{ get; set;}
        public virtual ICollection<Category>? CategoryRelationList { get; set; }
        public virtual ICollection<BookCategory>? BookCatagoryRelationList { get; set; }

        [NotMapped] // Sirve para que no se toma en cuenta este atributo para el mapeo en la tabla de la base de datos
        public List<int>? Categories { get; set; }
        [NotMapped]
        public string? CategoriesNames { get; set; }
        
    }
}