using System;
using Test.CQRS.WriteSide.Common.Domain;

namespace Test.CQRS.Infrastructure.Storage
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
    }
}
