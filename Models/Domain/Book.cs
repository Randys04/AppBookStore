using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        
    }
}