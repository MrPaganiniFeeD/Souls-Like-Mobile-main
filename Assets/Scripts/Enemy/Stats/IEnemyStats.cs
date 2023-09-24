namespace DefaultNamespace.Bot.Stats
{
    public interface IEnemyStats
    {
        EnemyStat Health { get; }
        EnemyStat Damage { get; }
        EnemyStat Protection { get; }
    }
}