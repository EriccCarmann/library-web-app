1. Setting up the database. 
   LibraryWebApi -> appsettings.json -> "ConnectionStrings": {
"DefaultConnection": "Server=SASHA-PC;Database=LibraryWebAppDB;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true"} 
change "SASHA-PC" to your database name.

2. To run the project:
   Click right mouse button on LibraryWebApi in the Solution Explorer -> Set as Startup project -> F5
or
   Tools -> Command Line -> Developer Command Prompt -> type "cd LibraryWebApi" -> type "dotnet watch run"

*The database has been populated with data and has 3 authors, 9 books and 1 admin account. The Seed() method is located at Library.Infrastructure/Persistence/ApplicationDBContext.cs

