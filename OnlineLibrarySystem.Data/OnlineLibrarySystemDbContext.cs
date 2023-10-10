using Microsoft.EntityFrameworkCore;
using OnlineLibrarySystem.Data.Models;

namespace OnlineLibrarySystem.Data;

public class OnlineLibrarySystemDbContext : DbContext
{
    public OnlineLibrarySystemDbContext(DbContextOptions<OnlineLibrarySystemDbContext> context) : base(context)
    {
        
    }
    
    public DbSet<Book> Books { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../OLS.db");
    }
}