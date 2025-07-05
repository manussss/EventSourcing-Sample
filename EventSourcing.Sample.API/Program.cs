using EventSourcing.Sample.Domain.Contracts;
using EventSourcing.Sample.Domain.Entities;
using EventSourcing.Sample.Domain.Events;
using EventSourcing.Sample.Infra.Data;
using EventSourcing.Sample.Infra.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderEventStore, OrderEventStore>();
builder.Services.AddDbContext<OrderDbContext>(options => options.UseInMemoryDatabase("OrdersEventStore"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/v1/orders", async (
    [FromServices] IOrderEventStore orderEventStore
) =>
{
    var orderId = Guid.NewGuid();
    var @event = new OrderCreated
    {
        OrderId = orderId,
        Data = "{}"
    };
    await orderEventStore.AppendEvent(@event);

    return Results.Ok(new { orderId });
});

app.MapPost("/api/v1/orders/{orderId}/confirm", async (
    [FromServices] IOrderEventStore orderEventStore,
    Guid orderId
) =>
{
    var events = await orderEventStore.GetEventsForOrder(orderId);

    var order = Order.RebuildFromEvents(events);

    if (order.Status == "Confirmed")
        return Results.BadRequest("Already confirmed");

    var @event = new OrderConfirmed
    {
        OrderId = orderId,
        Data = "{}"
    };

    await orderEventStore.AppendEvent(@event);

    return Results.Ok();
});

app.MapGet("/api/v1/orders/{orderId}", async (
    [FromServices] IOrderEventStore orderEventStore,
    Guid orderId
) =>
{
    var events = await orderEventStore.GetEventsForOrder(orderId);
    var order = Order.RebuildFromEvents(events);

    return Results.Ok(order);
});

await app.RunAsync();