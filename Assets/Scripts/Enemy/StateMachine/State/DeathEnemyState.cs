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

    public DeathEnemyState(List<ITransition> transitions,
        EnemyStateAnimator stateAnimator,
        Stat health,
        AudioSource audioSource,
        RagdollOperations ragdollOperations) 
        : base(transitions, stateAnimator)
    {
        _health = health;
        _audioSource = audioSource;
        _ragdollOperations = ragdollOperations;
    }

    public override void Enter()
    {
        base.Enter();
        _health.Value = 0;
        StateAnimator.Disable();
        _ragdollOperations.EnableRagdoll();
    }
}