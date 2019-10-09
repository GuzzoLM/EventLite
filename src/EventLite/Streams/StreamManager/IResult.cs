namespace EventLite.Streams.StreamManager
{
    /// <summary>
    /// The Result of a Stream Manager operation
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// The final state of the operation
        /// </summary>
        bool Success { get; }
        FailureReason FailureReason { get; }
    }

    /// <summary>
    /// The Result of a Stream Manager operation
    /// </summary>
    /// <typeparam name="T">The type of data the operation returns</typeparam>
    public interface IResult<T> : IResult
    {
        /// <summary>
        /// The data returned in the operation
        /// </summary>
        T Data { get; }
    }

    public enum FailureReason
    {
        None,
        ConcurrencyError,
        ConnectivityError
    }
}