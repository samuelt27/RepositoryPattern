using Microsoft.EntityFrameworkCore;
using RepositoryPattern.EntityFrameworkCore.Entities;

namespace RepositoryPattern.EntityFrameworkCore.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();


    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().AreUnicode(false);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>(e =>
        {
            e.Property(c => c.Name)
                .HasMaxLength(50);
        });

        builder.Entity<Product>(e =>
        {
            e.Property(p => p.Name)
                .HasMaxLength(100);

            e.Property(p => p.Price)
                .HasPrecision(11, 2);

            e.Property(p => p.Description)
                .HasMaxLength(1000);

            e.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.ClientCascade);
        });
        
        base.OnModelCreating(builder);
    }
}