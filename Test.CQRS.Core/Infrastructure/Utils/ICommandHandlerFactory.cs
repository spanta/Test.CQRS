using Test.CQRS.WriteSide.Common.Commands;

namespace Test.CQRS.Infrastructure.Utils
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> GetHandler<T>() where T : Command;
    }
}
