using DQ.ConsoleApp.AppCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace DQ.ConsoleApp.AppCore.Persistence;

public sealed class AppDbContext : DbContext
{
    public DbSet<Customer> Customers => this.Set<Customer>();
    public DbSet<Order> Orders => this.Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(
            entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.HasMany(x => x.Orders)
                    .WithOne(x => x.Customer)
                    .HasForeignKey(x => x.CustomerId);
            });

        modelBuilder.Entity<Order>(
            entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Amount)
                    .HasPrecision(18, 2);

                entity.Property(x => x.CreatedAt)
                    .IsRequired();
            });
    }
}
