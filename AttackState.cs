using System;
using System.Collections.Generic;
using Fabrics;
using Infrastructure.Services;
using Inventory.InventoryWithSlots.Equipped;
using PlayerLogic.Animation;
using PlayerLogic.States.State;
using PlayerLogic.States.StateMachine;
using PlayerLogic.States.Transition;
using UnityEngine;

public class AttackState : PlayerState, IRotateState, IAttackState, IDisposable
{
    private readonly PlayerStateAnimator _animator;
    private readonly PlayerStateMachine _playerStateMachine;
    private readonly IInputService _inputService;
    public override float Duration { get; } = 0.5f;

    private FabricPlayerStates _fabricStates;
    private int _counterLeftHandCombo;
    private int _counterRightHandCombo;
    private int _counterCombo;
        
    private float _timeCombo;
    private float _lastComboEnd;
    private float _lastClikedLeftButtonTime;
    private float _lastClikedRightButtonTime;

    private bool _isClickedLeftButton;
    private bool _isClickedRightButton;
    private bool _isClickedAttackButton;
        
        
    private readonly IWeaponInfo _rightWeapon;
    private readonly IWeaponInfo _leftWeapon;

    public AttackState(List<ITransition> transitions, PlayerStateAnimator animator, IInputService inputService,
        InventoryEquipped inventory, 
        PlayerStateMachine playerStateMachine) : base(transitions)
    {
        for (int i = 0; i < transitions.Count; i++)
        {
            Debug.Log(transitions[i]);
        }
        _animator = animator;
        _playerStateMachine = playerStateMachine;
        _inputService = inputService;
        _rightWeapon = inventory.RightHand.Unarmed;
        _leftWeapon = inventory.LeftHand.Unarmed;
    }

    private void OnExitAttack()
    {
        Debug.Log(_isClickedLeftButton);
        Debug.Log(_counterCombo);
        if (_isClickedLeftButton && _counterCombo + 1 < _leftWeapon.AttackInfos.Count)
        {
            _counterCombo++;
            Attack();
        }
        else
        {
            Debug.Log("Else ? On ExitAttack");
            EndAttack();
        }
    }

    private void Attack()
    {
        _animator.StartAttackAnimation(_counterCombo);
        _isClickedLeftButton = false;
        _isClickedRightButton = false;
        Debug.Log("Next attack Start");
        _inputService.LeftHandAttackButtonUp += OnLeftHandAttackButtonUp;
        _inputService.RightHandAttackButtonUp += OnRightHandAttackButtonUp;
        if (_counterCombo + 1 == _leftWeapon.AttackInfos.Count)
        {
            EndAttack();
        }
    }
    private void OnExitRightHandAttack()
    {
            
    }

    private void OnExitLeftHandAttack()
    {
            
    }
    private void EndAttack()
    {
        _playerStateMachine.Enter<IdleState>();
        _counterCombo = 0;
        _inputService.LeftHandAttackButtonUp -= OnLeftHandAttackButtonUp;
        _inputService.RightHandAttackButtonUp -= OnRightHandAttackButtonUp;
        _isClickedLeftButton = false;
        _isClickedRightButton = false;
    }

    public void Enter(IAttackStatePayloaded payloaded)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Subscribe();
        Attack();
    }

    private void OnRightHandAttackButtonUp()
    {
        _isClickedRightButton = true;
    }

    private void OnLeftHandAttackButtonUp()
    {
        Debug.Log("Click attack button");
        _isClickedLeftButton = true;
    }

    public void Rotate(Vector3 direction)
    {
        throw new NotImplementedException();
    }

    public void Dispose() => 
        Unsubscribe();

    public override void Exit()
    {
        base.Exit();
        _animator.StopAttackAnimation();
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        _animator.ExitRightHandAttack -= OnExitRightHandAttack;
        _animator.ExitLeftHandAttack -= OnExitLeftHandAttack;
        _animator.ExitAttack -= OnExitAttack;
        _inputService.LeftHandAttackButtonUp -= OnLeftHandAttackButtonUp;
        _inputService.RightHandAttackButtonUp -= OnRightHandAttackButtonUp;
    }
    private void Subscribe()
    {
        _animator.ExitRightHandAttack += OnExitRightHandAttack;
        _animator.ExitLeftHandAttack += OnExitLeftHandAttack;
        _animator.ExitAttack += OnExitAttack;
    }
}