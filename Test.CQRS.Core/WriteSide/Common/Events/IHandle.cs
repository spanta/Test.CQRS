namespace Test.CQRS.WriteSide.Common.Events
{
    public interface IHandle<TEvent> where TEvent:Event
    {
        void Handle(TEvent e);
    }
}
