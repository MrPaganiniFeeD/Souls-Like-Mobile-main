using System.Collections.Generic;
using PlayerLogic.States.Transition;
using UnityEngine;

public class AttackEnemyState : EnemyState
{
    private readonly AttackEnemyStateData _attackEnemyStateData;
    private readonly Transform _player;
    private readonly Transform _selfTransform;
    private readonly EnemyStateMachine _stateMachine;
    private readonly AudioSource _audioSource;
    private readonly IMoveModule _moveModule;
    private int _numberAttack;

    public AttackEnemyState(List<ITransition> transitions,
        EnemyStateAnimator stateAnimator,
        AttackEnemyStateData attackEnemyStateData,
        Transform player,
        Transform selfTransform,
        EnemyStateMachine stateMachine, 
        AudioSource audioSource,
        IMoveModule moveModule)
        : base(transitions, stateAnimator)
    {
        _attackEnemyStateData = attackEnemyStateData;
        _player = player;
        _selfTransform = selfTransform;
        _stateMachine = stateMachine;
        _audioSource = audioSource;
        _moveModule = moveModule;
    }

    public override void Enter()
    {
        base.Enter();
        _numberAttack = 0;
        StateAnimator.StartAttackAnimation(_numberAttack);
        PlayAudioClip(_numberAttack);
        StateAnimator.AttackIsOver += TryNextAttack;
    }

    private void PlayAudioClip(int numberAttack)
    {
        _audioSource.clip = _attackEnemyStateData.Attacks[numberAttack].AttackAudioClip;
        _audioSource.volume = 0.4f;
        _audioSource.PlayDelayed(0.13f);
    }

    public override void Exit()
    {
        base.Exit();
        StateAnimator.AttackIsOver -= TryNextAttack;
    }

    private void TryNextAttack()
    {
        if(_numberAttack + 1 <  _attackEnemyStateData.Attacks.Count)
            if(Vector3.Distance(_selfTransform.position,_player.position)
                <= _attackEnemyStateData.Attacks[_numberAttack + 1].AttackDistance)
            {
                _numberAttack++;
                _moveModule.Rotate(_player);
                PlayAudioClip(_numberAttack);
                StateAnimator.StartAttackAnimation(_numberAttack);
            }
            else
            {
                _stateMachine.Enter<IdleEnemyState>();
                _numberAttack = 0;
            }
        else
        {
            _stateMachine.Enter<IdleEnemyState>();
            _numberAttack = 0;
        }
    }
}