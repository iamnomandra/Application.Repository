# Generic Repository

`Purpose`: “Generic Repository for reusable CRUD operations in .NET Core, reducing boilerplate code.”</br>
`Features`: “Type-safe CRUD, dependency injection, async support, works with any entity (e.g., User, Product).”</br>
`Tech Stack`: .NET Core, EF Core, dependency injection.</br>
`Setup`: dotnet restore, dotnet ef migrations add Initial, dotnet run.  

```csharp
public async Task<T> AddAsync(T entity)
{ 
    try
    {
        await mDbContext.Database.BeginTransactionAsync();
        await mDbSet.AddAsync(entity);
        await mDbContext.SaveChangesAsync();
        await mDbContext.Database.CommitTransactionAsync();
    }
    catch(Exception e)
    {
        var error = e.InnerException == null ? e : e.InnerException;
        mDbContext.Database.RollbackTransaction(); 
        throw error;
    }
    finally
    {
        mDbContext.Database.BeginTransactionAsync().Dispose();
        this.mDbContext.Dispose();
    }
    return entity;
}
```

# Screenshot

**`Home` : Home endpoint for API health check** 

<img src="https://github.com/iamnomandra/generic-repository/blob/main/Screenshot%202025-05-11%20223120.png" width="800">  
 
**`End points`** 

<img src="https://github.com/iamnomandra/generic-repository/blob/main/Screenshot%202025-05-11%20213141.png" width="800">

**`Api Response`**  

<img src="https://github.com/iamnomandra/generic-repository/blob/main/Screenshot%202025-05-11%20220135.png" width="800">  
