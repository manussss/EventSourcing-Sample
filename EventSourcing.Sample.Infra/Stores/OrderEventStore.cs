using EventSourcing.Sample.Domain.Contracts;
using EventSourcing.Sample.Domain.Events;
using EventSourcing.Sample.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Sample.Infra.Stores;

public class OrderEventStore(OrderDbContext dbContext) : IOrderEventStore
{
    private readonly OrderDbContext _dbContext = dbContext;

    public async Task AppendEvent(OrderEvent @event)
    {
        _dbContext.OrderEvents.Add(@event);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<OrderEvent>> GetEventsForOrder(Guid orderId)
    {
        return await _dbContext.OrderEvents
            .Where(e => e.OrderId == orderId)
            .OrderBy(e => e.Timestamp)
            .ToListAsync();
    }
}