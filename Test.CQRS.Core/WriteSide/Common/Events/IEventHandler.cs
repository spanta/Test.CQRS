namespace Test.CQRS.WriteSide.Common.Events
{
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        void Handle(TEvent handle);
    }
}
