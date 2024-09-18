using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Entities.Urls;

namespace URLShortener.Persistence.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Url> Urls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Url>(builder =>
        {
            builder.Property(s => s.Code).HasMaxLength(6);
            builder.HasIndex(s => s.Code).IsUnique();
        });
    }
}