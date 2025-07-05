using EventSourcing.Sample.Domain.Events;

namespace EventSourcing.Sample.Domain.Entities;

public class Order
{
    public Guid OrderId { get; set; }
    public string Status { get; set; } = "Pending";

    public static Order RebuildFromEvents(IEnumerable<OrderEvent> events)
    {
        var order = new Order();

        foreach (var @event in events.OrderBy(e => e.Timestamp))
        {
            switch (@event)
            {
                case OrderCreated created:
                    order.OrderId = created.OrderId;
                    order.Status = "Pending";
                    break;
                case OrderConfirmed confirmed:
                    order.Status = "Confirmed";
                    break;
            }
        }
        
        return order;
    }
}