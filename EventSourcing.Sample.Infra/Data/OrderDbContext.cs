using EventSourcing.Sample.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Sample.Infra.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<OrderEvent> OrderEvents => Set<OrderEvent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<OrderEvent>()
            .HasKey(e => e.Id);

        modelBuilder
            .Entity<OrderEvent>()
            .Property(e => e.Data);

        modelBuilder
            .Entity<OrderEvent>()
            .HasDiscriminator<string>("EventType")
            .HasValue<OrderCreated>(nameof(OrderCreated))
            .HasValue<OrderConfirmed>(nameof(OrderConfirmed));
    }
}