using Hero.States.Transition;
using UnityEngine;

public class FollowTransition : ITransition
{
    private Transform _player;
    private Transform _selfTransform;
    private float _foundDistance;
    private EnemyStateMachine _enemyStateMachine;

    public FollowTransition(EnemyStateMachine enemyStateMachine,
        Transform player,
        Transform selfTransform,
        FollowTransitionDate followTransitionDate)
    {
        _enemyStateMachine = enemyStateMachine;
        _player = player;
        _selfTransform = selfTransform;
        _foundDistance = followTransitionDate.FoundDistance;
    }

    public void Enter() { }

    public void Update() =>
        UpdateTargetPosition(_player);

    public void Exit() { }

    public void Transit() => 
        _enemyStateMachine.Enter<FollowEnemyState>();

    private void UpdateTargetPosition(Transform targetTransform)
    {
        if(Vector3.Distance(targetTransform.position, _selfTransform.position) < _foundDistance)
            Transit();
    }

}
