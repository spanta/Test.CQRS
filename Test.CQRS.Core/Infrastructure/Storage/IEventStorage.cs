using System;
using System.Collections.Generic;
using Test.CQRS.WriteSide.Common.Domain;
using Test.CQRS.WriteSide.Common.Events;

namespace Test.CQRS.Infrastructure.Storage
{
    public interface IEventStorage
    {
        IEnumerable<Event> GetEvents(Guid aggregateId);
        void Save(AggregateRoot aggregate);
        T GetMemento<T>(Guid aggregateId) where T: BaseMemento;
        void SaveMemento(BaseMemento memento);
    }
}
