using ER.Huawei.Integrator.Cons.Domain.Core.Bus;
using ER.Huawei.Integrator.Cons.Infraestructure;
using ER.Huawei.Integrator.Domain.EventHandlers;
using ER.Huawei.Integrator.Domain.Events;
using MediatR;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

//Domain Bus
builder.Services.AddSingleton<IEventBus, RabbitMQBus>(sp => {
    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
});
builder.Services.AddTransient<IEventHandler<PlantDeviceResultEvent>, PlantDeviceResultEventHandler>();
//Subscriptions
builder.Services.AddTransient<PlantDeviceResultEventHandler>();

var app = builder.Build();
var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<PlantDeviceResultEvent, PlantDeviceResultEventHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();



