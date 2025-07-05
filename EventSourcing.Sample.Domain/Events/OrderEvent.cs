namespace EventSourcing.Sample.Domain.Events;

public abstract class OrderEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public abstract string EventType { get; }
    public string Data { get; set; }
}