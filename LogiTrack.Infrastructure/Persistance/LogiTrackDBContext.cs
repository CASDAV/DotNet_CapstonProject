using LogiTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogiTrack.Infrastructure.Persistance;

internal class LogiTrackDBContext : DbContext
{
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<Order> Orders { get; set; }

    public LogiTrackDBContext(DbContextOptions<LogiTrackDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<InventoryItem>()
            .HasIndex(x => x.OrderId);
    }
}
