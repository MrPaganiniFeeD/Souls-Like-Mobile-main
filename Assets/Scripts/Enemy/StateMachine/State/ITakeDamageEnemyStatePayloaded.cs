public interface ITakeDamageEnemyStatePayloaded : IEnemyStatePayloaded
{
    int Damage { get; }
}

public class TakeDamageEnemyStatePayloaded : ITakeDamageEnemyStatePayloaded
{
    public int Damage { get; }

    public TakeDamageEnemyStatePayloaded(int damage)
    {
        Damage = damage;
    }
}