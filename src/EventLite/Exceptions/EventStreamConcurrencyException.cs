using System;

namespace EventLite.Exceptions
{
    /// <summary>
    /// This exception occurs when the persisted state of the stream was modified before the applicaton could save the changes.
    /// Raise the aggregate again e reapply the changes.
    /// </summary>
    public class EventStreamConcurrencyException : Exception
    {
        public override string Message => _message;

        private readonly string _message;

        private const string _defaultMessage = "Stream state changed before changes were persisted.";

        public EventStreamConcurrencyException()
            : base()
        {
            _message = _defaultMessage;
        }

        public EventStreamConcurrencyException(string message)
            : base(message)
        {
            _message = _defaultMessage + Environment.NewLine + message;
        }

        public EventStreamConcurrencyException(string message, Exception inner)
            : base(message, inner)
        {
            _message = _defaultMessage + Environment.NewLine + message;
        }
    }
}