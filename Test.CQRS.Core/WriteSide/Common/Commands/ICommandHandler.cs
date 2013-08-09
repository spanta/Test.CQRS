namespace Test.CQRS.WriteSide.Common.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        void Execute(TCommand command);
    }
}
