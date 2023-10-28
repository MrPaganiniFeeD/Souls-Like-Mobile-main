using System;
using System.Collections.Generic;
using Hero.States.Transition;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemyState : EnemyState, IDisposable
{
    private readonly EnemyStateAnimator _stateAnimator;
    private readonly NavMeshAgent _agent;
    private readonly Transform _player;
    private readonly FollowStateDate _followStateDate;
    private readonly AudioSource _audioSource;
    private float _minimalDistance;
    private float _speed;
    private Transform _transform;

    public FollowEnemyState(List<ITransition> transitions,
        EnemyStateAnimator stateAnimator,
        NavMeshAgent agent,
        Transform player,
        FollowStateDate followStateDate,
        AudioSource audioSource) : base(transitions, stateAnimator)
    {
        _stateAnimator = stateAnimator;
        _agent = agent;
        _player = player;
        _followStateDate = followStateDate;
        _audioSource = audioSource;
        _speed = followStateDate.Speed;
        _minimalDistance = followStateDate.MinimalDistance;

        agent.speed = _speed;
        _transform = _agent.GetComponent<Transform>();

        _stateAnimator.StepFootLR += PlayAudioClip;
    }

    public override void Enter()
    {
        base.Enter();
        _stateAnimator.StartFollowAnimation();
    }

    public override void Update()
    {
        base.Update();
        if (PlayerNotReached())
            _agent.destination = _player.position;
        _stateAnimator.UpdateSpeed(_agent.speed);
        _stateAnimator.UpdateVelocity(_transform.forward.x, Math.Abs(_transform.forward.z));
    }

    public override void Exit()
    {
        base.Exit();
        _stateAnimator.StopFollowAnimation();
    }

    private void PlayAudioClip()
    {
        _audioSource.volume = 0.15f;
        _audioSource.PlayOneShot(_followStateDate.StepAudioClip);
    }

    private bool PlayerNotReached() => 
        Vector3.Distance(_agent.transform.position, _player.position) >= _followStateDate.MinimalDistance;


    public void Dispose()
    {  
        _stateAnimator.StepFootLR -= PlayAudioClip;
    }
}