namespace EventSourcing.Sample.Domain.Events;

public abstract class OrderEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string EventType { get; protected set; }
    public string Data { get; set; }
}