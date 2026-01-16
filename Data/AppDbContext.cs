using Microsoft.EntityFrameworkCore;
using VideoClub.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<TiposArticulos> TiposArticulos { get; set; }

 
}

