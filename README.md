# LinkHolder

## Creating the standart WEB API application

    dotnet new webapi

## Creating the user model
Create a file `UserModels.cs` in the folder `Models`

## Creating the store of the links model
Create a file `Link.cs` in the folder `Models`

## Creating the database context
Create a file `AppDbContext.cs` in the folder `Models`
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
Add `AddDbContext` in the file `Startup.cs`
Add `Identity` in the file `Startup.cs`

## Creating the database

    dotnet ef  migrations add CreateDatabase
    dotnet ef database update

## Creating the Authentication

Create the file `AuthProperties.cs` in the folder `Models`
Create the file `AccountController.cs` in the folder `Controller`
Write `Token` controller for JWT Authentication
Add JWT-functional in in the file `Startup.cs` (Authentication, Authorization)

## User creation

Add method `Register` in the `AccountController.cs`
Add class `CreateUserModel` in the `UserModels.cs`

## Administration

Create the file `AdminController.cs` in the folder `Controller`
Create the file `RoleAdminController.cs` in the folder `Controller`
Add `EditUserModel` in the `UserModels.cs`
Add `RoleModificationModel` in the `UserModels.cs`

## The data for first deploy

Add `AdminUser` data to the `appsettings.json`
Add the method `CreateAdminAccount` in the file `AppDbContext.cs`
Add the method `CreateAdminAccount` in the file `Startup.cs`
Add the `UseDefaultServiceProvider` in the file `Program.cs`