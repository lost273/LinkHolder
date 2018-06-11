# LinkHolder

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

## Adding A Migration

    dotnet ef  migrations add CreateDatabase
    dotnet ef database update


