namespace EventSourcing.Sample.Domain.Events;

public class OrderCreated : OrderEvent
{
    public OrderCreated()
    {
        EventType = nameof(OrderCreated);
    }
}