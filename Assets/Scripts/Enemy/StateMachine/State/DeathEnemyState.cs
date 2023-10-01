using System.Collections.Generic;
using NTC.Global.System;
using PlayerLogic.States.Transition;
using PlayerLogic.Stats;
using UnityEngine;

public class DeathEnemyState : EnemyState
{
    private readonly Stat _health;
    private readonly AudioSource _audioSource;
    private readonly RagdollOperations _ragdollOperations;
    private readonly Lockable _lockable;

    public DeathEnemyState(List<ITransition> transitions,
        EnemyStateAnimator stateAnimator,
        Stat health,
        AudioSource audioSource,
        RagdollOperations ragdollOperations,
        Lockable lockable) 
        : base(transitions, stateAnimator)
    {
        _health = health;
        _audioSource = audioSource;
        _ragdollOperations = ragdollOperations;
        _lockable = lockable;
    }

    public override void Enter()
    {
        base.Enter();
        _health.Value = 0;
        _lockable.SetState(false);
        StateAnimator.Disable();
        _ragdollOperations.EnableRagdoll();
    }
}