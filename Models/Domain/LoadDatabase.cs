using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AppBookStore.Models.Domain
{
    public class LoadDatabase
    {
        public static async Task InsertData(DatabaseContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("ADMIN"));
            }

            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    Name = "Randy Moya",
                    Email = "randym09@gmail.com",
                    UserName = "Randys"
                };

                await userManager.CreateAsync(user, "PasswordRandys1234?");
                await userManager.AddToRoleAsync(user, "ADMIN");
            }

            if(!context.Categories.Any())
            {
                await context.Categories.AddRangeAsync(
                    new Category {Name = "Drama"},
                    new Category {Name = "Terror"},
                    new Category {Name = "Comedia"},
                    new Category {Name = "Accion"},
                    new Category {Name = "Aventura"}
                );

                await context.SaveChangesAsync();
            }

            if(!context.Books.Any())
            {
                await context.Books.AddRangeAsync(
                    new Book {
                        Title = "Quijote de la Mancha",
                        CreateDate = DateTime.Now.ToString(),
                        CoverImage = "quijote.jpg",
                        Author = "Miguel de Cervantes"
                    },
                    new Book {
                        Title = "Harry Potter",
                        CreateDate = DateTime.Now.ToString(),
                        CoverImage = "harry.jpg",
                        Author = "Juan de la Vega"
                    }
                );

                await context.SaveChangesAsync();
            }

            if(!context.BooksCategories.Any())
            {
                await context.BooksCategories.AddRangeAsync(
                    new BookCategory {
                        CategoryId = 1,
                        BookId = 1
                    },
                    new BookCategory {
                        CategoryId = 1,
                        BookId = 2
                    }
                );

                await context.SaveChangesAsync();
            }

            context.SaveChanges();
        }
        
    }
}