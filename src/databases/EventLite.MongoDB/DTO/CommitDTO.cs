﻿using System;

namespace EventLite.MongoDB.DTO
{
    internal class CommitDTO
    {
        public Guid StreamId { get; set; }

        public int CommitNumber { get; set; }

        public long Timestamp { get; set; }

        public object Event { get; set; }
    }
}