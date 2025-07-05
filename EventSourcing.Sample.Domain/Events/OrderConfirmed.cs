namespace EventSourcing.Sample.Domain.Events;

public class OrderConfirmed : OrderEvent
{
    public override string EventType => nameof(OrderConfirmed);
}