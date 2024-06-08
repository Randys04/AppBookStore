using AppBookStore.Models;
using AppBookStore.Models.Domain;
using AppBookStore.Repositories.Abstract;
using AppBookStore.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// inicializando la base de datos e imprimimos en consola todas las consultas a la base de datos
builder.Services.AddDbContext<DatabaseContext> (opt => {
    opt.LogTo(Console.WriteLine, new [] {
        DbLoggerCategory.Database.Command.Name},
        LogLevel.Information).EnableSensitiveDataLogging();

        opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteDatabase"));
});

// Injeccion
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // Hace posible trabajar con el login y la generacion de tokens o cokies
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



using (var enviroment = app.Services.CreateScope()) // Carga la data master
{
    try
    {
        var services = enviroment.ServiceProvider;
        var context = services.GetRequiredService<DatabaseContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await context.Database.MigrateAsync(); // ejecuta los archivos de migracion

        await LoadDatabase.InsertData(context, userManager, roleManager);
    }
    catch (System.Exception)
    {
        Console.WriteLine("There was an error trying to load the data");
    }
};

app.Run();
