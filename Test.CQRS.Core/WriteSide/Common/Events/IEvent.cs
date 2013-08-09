using System;

namespace Test.CQRS.WriteSide.Common.Events
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}
