using System;

namespace Test.CQRS.Infrastructure.Exceptions
{
    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message) : base(message) { }
    }
}
