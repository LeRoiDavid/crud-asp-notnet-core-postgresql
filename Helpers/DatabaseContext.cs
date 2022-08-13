namespace ApiUser.Helpers;

using Microsoft.EntityFrameworkCore;

using ApiUser.Entities;

public class DatabaseContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DatabaseContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }
    
    
    public DbSet<Produit> Produits { get; set; }
    
    
    
}