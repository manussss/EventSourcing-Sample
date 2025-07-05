namespace EventSourcing.Sample.Domain.Events;

public class OrderConfirmed : OrderEvent
{
    public OrderConfirmed()
    {
        EventType = nameof(OrderConfirmed);
    }
}