using System.Collections.Generic;
using Test.CQRS.WriteSide.Common.Events;

namespace Test.CQRS.Infrastructure.Utils
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event;
    }
}
