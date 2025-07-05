namespace EventSourcing.Sample.Domain.Events;

public class OrderCreated : OrderEvent
{
    public override string EventType => nameof(OrderCreated);
}