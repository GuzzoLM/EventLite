using System;

namespace EventLite
{
    /// <summary>
    /// This interface must be implemented by any event handled by the aggregate.
    /// </summary>
    public interface IRaisedEvent
    {
        DateTime Timstamp { get; }
        string EventType { get; }
    }
}