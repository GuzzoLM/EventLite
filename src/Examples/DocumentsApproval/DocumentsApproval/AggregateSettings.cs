using EventLite;

namespace DocumentsApproval
{
    public class AggregateSettings : IAggregateSettings
    {
        public int CommitsBeforeSnapshot { get; set; }
    }
}