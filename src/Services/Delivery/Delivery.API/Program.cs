using Common.Application.Commands;
using Delivery.Application.Commands;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/couriers/availability", async ([FromBody] UpdateCourierAvailability.Command command) =>
{
    var commandBus = new CommandBus(null);
    await commandBus.Send(command);
})
.WithName("UpdateCourierAvailability")
.WithOpenApi();

app.Run();