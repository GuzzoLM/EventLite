namespace EventLite
{
    public interface IEventHandler<T> where T : IRaisedEvent
    {
        void Apply(T @event);
    }
}