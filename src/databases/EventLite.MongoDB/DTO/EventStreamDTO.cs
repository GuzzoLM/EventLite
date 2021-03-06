﻿using System;

namespace EventLite.MongoDB.DTO
{
    internal class EventStreamDTO
    {
        public Guid StreamId { get; set; }

        public int HeadRevision { get; set; }

        public int UnsnapshottedCommits { get; set; }

        public int SnapshotRevision { get; set; }
    }
}