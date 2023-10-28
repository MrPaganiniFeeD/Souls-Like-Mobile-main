namespace DefaultNamespace.Stats
{
    public interface IStatsInfo
    {
        string Name { get; }
        int BaseValue { get; }
        int CurrentValue { get; }
    }
}