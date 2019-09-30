using System;

namespace EventLite
{
    public interface IRaisedEvent
    {
        DateTime Timstamp { get; }
        string EventType { get; }
    }
}