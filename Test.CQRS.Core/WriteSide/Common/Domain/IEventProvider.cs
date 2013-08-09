using System.Collections.Generic;
using Test.CQRS.WriteSide.Common.Events;

namespace Test.CQRS.WriteSide.Common.Domain
{
    public interface IEventProvider
    {
        void LoadsFromHistory(IEnumerable<Event> history);
        IEnumerable<Event> GetUncommittedChanges();
    }
}
