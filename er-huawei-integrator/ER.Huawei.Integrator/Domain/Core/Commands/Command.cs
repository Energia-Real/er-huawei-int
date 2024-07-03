using ER.Huawei.Integrator.Cons.Domain.Core.Events;

namespace ER.Huawei.Integrator.Cons.Domain.Core.Commands;

public abstract class Command : Message
{
    public DateTime Timestamp { get; protected set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }
}
