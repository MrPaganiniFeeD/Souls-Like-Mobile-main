using Hero.States.Transition;
using UnityEngine;

public class AttackEnemyTransition : ITransition
{
    private Transform _selfTransform;
    private readonly EnemyStateMachine _enemyStateMachine;
    private Transform _player;
    private float _attackDistance;

    public AttackEnemyTransition(EnemyStateMachine enemyStateMachine, 
        Transform player,
        Transform selfTransform,
        AttackEnemyDataTransition dataTransition)
    {
        _enemyStateMachine = enemyStateMachine;
        _player = player;
        _selfTransform = selfTransform;
        _attackDistance = dataTransition.AttackDistance;
    }

    public void Enter() { }

    public void Update() =>
        CheckAttackDistance();

    public void Exit() { }

    private void CheckAttackDistance()
    {
        if(Vector3.Distance(_selfTransform.position,_player.position) <= _attackDistance)
            Transit();
    }

    private void Transit() => 
        _enemyStateMachine.Enter<AttackEnemyState>();
}