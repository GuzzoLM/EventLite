using EventLite;

namespace POC.Documents
{
    public class AggregateSettings : IAggregateSettings
    {
        public int CommitsBeforeSnapshot { get; set; }
    }
}