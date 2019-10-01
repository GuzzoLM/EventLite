namespace EventLite
{
    /// <summary>
    /// Aggregate settings
    /// </summary>
    public interface IAggregateSettings
    {
        /// <summary>
        /// The number of commits before a snapshot is created
        /// </summary>
        int CommitsBeforeSnapshot { get; }
    }
}