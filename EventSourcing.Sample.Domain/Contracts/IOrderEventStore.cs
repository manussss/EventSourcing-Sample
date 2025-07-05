using EventSourcing.Sample.Domain.Events;

namespace EventSourcing.Sample.Domain.Contracts;

public interface IOrderEventStore
{
    Task AppendEvent(OrderEvent @event);
    Task<List<OrderEvent>> GetEventsForOrder(Guid orderId);
}