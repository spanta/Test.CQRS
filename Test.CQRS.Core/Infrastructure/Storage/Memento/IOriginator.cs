using Test.CQRS.WriteSide.Common.Domain;

namespace Test.CQRS.Infrastructure.Storage.Memento
{
    public interface IOriginator
    {
        BaseMemento GetMemento();
        void SetMemento(BaseMemento memento);
    }
}
