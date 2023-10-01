using PlayerLogic.States.Transition;
using UnityEngine;

public class TakeDamageEnemyTransition : ITransition
{
    private IDamageDetection _damageDetection;
    private EnemyStateMachine _enemyStateMachine;

    public TakeDamageEnemyTransition(EnemyStateMachine enemyStateMachine, IDamageDetection damageDetection)
    {
        _enemyStateMachine = enemyStateMachine;
        _damageDetection = damageDetection;
    }
    public void Enter() => 
        _damageDetection.TakingDamage += OnReceivedDamage;

    public void Update() { }

    public void Exit() => 
        _damageDetection.TakingDamage -= OnReceivedDamage;
    
    private void OnReceivedDamage(int damage) => Transit(damage);

    private void Transit(int damage)
    {
        var takeDamagePayloaded = new TakeDamageStatePayloaded(damage);
        Debug.Log("Transition");
        _enemyStateMachine.Enter<TakeDamageEnemyState, ITakeDamageStatePayloaded>(takeDamagePayloaded);
    }
}
