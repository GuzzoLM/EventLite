namespace EventLite
{
    public interface IAggregateSettings
    {
        int CommitsBeforeSnapshot { get; }
    }
}