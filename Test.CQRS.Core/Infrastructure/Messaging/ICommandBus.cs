using Test.CQRS.WriteSide.Common.Commands;

namespace Test.CQRS.Infrastructure.Messaging
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : Command;
    }
}
