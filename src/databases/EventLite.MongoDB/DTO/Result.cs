using EventLite.Streams.StreamManager;

namespace EventLite.MongoDB.DTO
{
    internal class Result : IResult
    {
        public bool Success { get; private set; }

        public FailureReason FailureReason { get; private set; }

        protected Result(bool succes, FailureReason failureReason = FailureReason.None)
        {
            Success = succes;
            FailureReason = failureReason;
        }

        public static Result Successfull() => new Result(true);

        public static Result Failure(FailureReason failureReason) => new Result(false, failureReason);
    }

    internal class Result<T> : Result, IResult<T>
    {
        public T Data { get; private set; }

        protected Result(bool success, T data, FailureReason failureReason = FailureReason.None) : base(success, failureReason)
        {
            Data = data;
        }

        public static Result<T> Successfull(T data) => new Result<T>(true, data);

        public new static Result<T> Failure(FailureReason failureReason) => new Result<T>(false, default(T), failureReason);
    }
}