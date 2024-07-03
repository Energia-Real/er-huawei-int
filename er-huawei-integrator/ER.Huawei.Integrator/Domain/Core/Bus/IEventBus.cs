using ER.Huawei.Integrator.Cons.Domain.Core.Events;

namespace ER.Huawei.Integrator.Cons.Domain.Core.Bus;

public interface IEventBus
{
    void Subscribe<T, TH>()
        where T : Event
        where TH : IEventHandler<T>;

}
