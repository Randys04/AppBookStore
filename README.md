# Book Store

## Description
This is a book management system designed for libraries, built using the model-view-controller (MVC) architecture pattern with .NET and C#. This system allows users to efficiently manage a library's book collection, including adding, updating, deleting, and querying books. Additionally, it provides functionalities for category management, facilitating the organization and access to book information. The system uses Entity Framework (EF) for database interaction, ensuring robust and efficient data management.

## Main Features
- Book Management: Users can create, delete, edit, and display books.
- Category Management: Users can create, delete, edit, and display book categories.
- Authentication: Users can register and log in to access the application's functionalities.
- MVC Design: Implements the Model-View-Controller design pattern for clear separation of responsibilities and maintainable code.
- Entity Framework: Uses Entity Framework for data management, simplifying CRUD (Create, Read, Update, Delete) operations and providing intuitive mapping between data models and the database.
- SQLite: Uses SQLite as the database management system, offering a lightweight and easy-to-configure solution for data storage.

### Technologies Used
- ASP.NET Core MVC: Framework for developing web applications based on the MVC pattern.
- Entity Framework Core: ORM for .NET that facilitates data access and interaction with the database.
- SQLite: Lightweight and fast database, ideal for small to medium-sized applications.

## Installation 

1. Clone the repository:
```bash
git clone $ https://github.com/Randys04/AppBookStore.git
```

2. Navigate to your project directory:
```bash
cd your-project
```

3. Restore the project dependencies:
```bash
dotnet restore
```

4. Apply the Entity Framework migrations to set up the database:datos:
```bash
dotnet ef database update
```

## Usage 

**Note**: Before starting the application, modify the following lines in the LoadDatabase.cs file located in the Models/Domain folder:

```csharp
var user = new AppUser
{
	Name = "Your Name",
	Email = "Your Email",
	UserName = "Username"
};

await userManager.CreateAsync(user, "Password");
```
You should enter your name, email, username, and a secure password. Make sure to remember these details as you will need the username and password to log in.

1. To start the application, run:

```bash
dotnet run
```

## License 
This project is licensed under the MIT License. See the LICENSE file for more details.

## Resources and Additional Links
- [Official ASP.NET Core Documentation](https://learn.microsoft.com/es-es/aspnet/core/?view=aspnetcore-5.0 "Documentación Oficial de ASP.NET Core")

- [Entity Framework Core Documentation](https://learn.microsoft.com/es-es/ef/core/ "Documentación de Entity Framework Core")