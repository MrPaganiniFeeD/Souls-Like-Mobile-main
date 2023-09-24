public interface IEnemyState<TPayloaded> where TPayloaded : IEnemyStatePayloaded
{
    void Enter(TPayloaded payloaded);
}