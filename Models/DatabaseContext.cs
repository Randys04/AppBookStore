using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppBookStore.Models
{
    public class DatabaseContext : IdentityDbContext<AppUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>()
                .HasMany(x => x.CategoryRelationList)
                .WithMany(x => x.BookRelationList)
                .UsingEntity<BookCategory>(
                    j => j
                    .HasOne(p => p.Category)
                    .WithMany(p => p.BookCatagoryRelationList)
                    .HasForeignKey(p => p.CategoryId),
                    j => j
                    .HasOne(p => p.Book)
                    .WithMany(p => p.BookCatagoryRelationList)
                    .HasForeignKey(p => p.BookId),
                    j =>
                    {
                        j.HasKey(t => new {t.BookId, t.CategoryId});
                    }
                );

        }
        public DbSet<Book> Books { get; set; } // Nombres de la referencia deben ser en plural
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BooksCategories { get; set; }
    }
}