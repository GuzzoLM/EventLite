namespace EventLite
{
    /// <summary>
    /// Aggregate must implement this interface for each handled event
    /// </summary>
    /// <typeparam name="T">The typeof the event to be handled</typeparam>
    public interface IEventHandler<T> where T : IRaisedEvent
    {
        void Apply(T @event);
    }
}