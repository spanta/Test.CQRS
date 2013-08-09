using System;

namespace Test.CQRS.WriteSide.Common.Domain
{
    public class BaseMemento
    {
        public Guid Id { get; internal set; }
        public int Version { get; set; }
    }
}
