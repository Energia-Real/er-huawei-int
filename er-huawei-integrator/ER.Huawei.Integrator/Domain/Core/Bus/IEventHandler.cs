using ER.Huawei.Integrator.Cons.Domain.Core.Events;

namespace ER.Huawei.Integrator.Cons.Domain.Core.Bus;

public interface IEventHandler<in TEvent> : IEventHandler 
    where TEvent : Event
{
    Task Handle(TEvent @event);
}

public interface IEventHandler { }
