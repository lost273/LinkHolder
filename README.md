# LinkHolder

## Creating the standart WEB API application

    dotnet new webapi

## Creating the user model
Create a file `UserModels.cs` in the folder `Models`

## Creating the store of the links model
Create a file `Link.cs` in the folder `Models`

## Creating the database context
Create a file `AppDbContext.cs.cs` in the folder `Models`
Make `IdentityDbContext` and `DbSet` for store of the links

## Creating the connection string
 Move next code:

    "Data": {
        "LinkHolder":{
            "ConnectionString":
            "Server=localhost\\SQLEXPRESS;Database=LinkHolderDb;Trusted_Connection=True;MultipleActiveResultSets=true"
        }
    }

to the file `appsettings.json`

## Register the context
Create `AddDbContext` in the file `Startup.cs`

## Creating the database

    dotnet ef  migrations add CreateDatabase
    dotnet ef database update

## Creating the Authentication

Create the file `AuthProperties.cs` in the folder `Models`
Create the file `AccountController.cs` in the folder `Controller`
Write `Token` controller for JWT Authentication