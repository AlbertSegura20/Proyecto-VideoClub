using Microsoft.EntityFrameworkCore;
using VideoClub.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<TiposArticulos> TiposArticulos { get; set; }
    public DbSet<Generos> Generos { get; set; }
    public DbSet<Idiomas> Idiomas { get; set; }
    public DbSet<Elenco> Elenco { get; set; }
    // public DbSet<Articulos> Articulos { get; set; }

 
}
