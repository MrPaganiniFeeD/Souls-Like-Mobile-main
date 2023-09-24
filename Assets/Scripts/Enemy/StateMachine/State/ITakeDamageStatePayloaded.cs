public interface ITakeDamageStatePayloaded : IEnemyStatePayloaded
{
    int Damage { get; }
}

public class TakeDamageStatePayloaded : ITakeDamageStatePayloaded
{
    public int Damage { get; }

    public TakeDamageStatePayloaded(int damage)
    {
        Damage = damage;
    }
}