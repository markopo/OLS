using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OnlineLibrarySystem.Data;


public class OnlineLibrarySystemContextFactory: IDesignTimeDbContextFactory<OnlineLibrarySystemDbContext>
{
    public OnlineLibrarySystemDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OnlineLibrarySystemDbContext>();
        optionsBuilder.UseSqlite("Data Source=../OLS.db");
        return new OnlineLibrarySystemDbContext(optionsBuilder.Options);
    }
}