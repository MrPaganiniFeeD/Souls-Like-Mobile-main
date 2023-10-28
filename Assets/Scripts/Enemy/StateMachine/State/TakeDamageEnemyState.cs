using System.Collections.Generic;
using Hero.States.Transition;
using Hero.Stats;
using UnityEngine;

public class TakeDamageEnemyState : EnemyState, IEnemyState<ITakeDamageEnemyStatePayloaded>
{
    private readonly BaseMoveModule _baseMoveModule;
    private readonly Rigidbody _rigidbody;
    private readonly EnemyStateMachine _stateMachine;
    private readonly Transform _transform;
    private readonly Stat _health;
    private readonly AudioSource _audioSource;
    private readonly AudioClip _takeDamageAudioClip;

    public TakeDamageEnemyState(List<ITransition> transitions,
        EnemyStateAnimator stateAnimator,
        Rigidbody rigidbody,
        EnemyStateMachine stateMachine,
        Transform transform,
        Stat health, 
        AudioSource audioSource,
        AudioClip takeDamageAudioClip) : base(transitions, stateAnimator)
    {
        _rigidbody = rigidbody;
        _stateMachine = stateMachine;
        _transform = transform;
        _health = health;
        _audioSource = audioSource;
        _takeDamageAudioClip = takeDamageAudioClip;
    }

    public void Enter(ITakeDamageEnemyStatePayloaded payloaded)
    {
        base.Enter();    
        if (_health.Value - payloaded.Damage > 0)
        {
            _health.Value -= payloaded.Damage;
            StateAnimator.StartApplyDamageAnimation();
            StateAnimator.EndGetHitAnimation += OnEndGetHitAnimation;
            _rigidbody.AddForce(-_transform.forward * 2, ForceMode.Force);
            PlayAudioTakeDamageClip();
        }
        else
        {
            PlayAudioTakeDamageClip();
            _stateMachine.Enter<DeathEnemyState>();
        }
    }

    private void PlayAudioTakeDamageClip()
    {
        _audioSource.clip = _takeDamageAudioClip;
        _audioSource.volume = 0.6f;
        _audioSource.PlayDelayed(0.15f);
    }

    public override void Exit()
    {
        base.Exit();
        StateAnimator.EndGetHitAnimation -= OnEndGetHitAnimation;
    }

    private void OnEndGetHitAnimation()
    {
        Debug.Log("On en get hit Animation");
        _stateMachine.Enter<IdleEnemyState>();
    }
}