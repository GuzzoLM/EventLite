﻿namespace EventLite
{
    /// <summary>
    /// Aggregate settings
    /// </summary>
    public interface IAggregateSettings
    {
        int SnapshotAtCommit { get; }
    }
}