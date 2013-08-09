using System;

namespace Test.CQRS.WriteSide.Common.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
