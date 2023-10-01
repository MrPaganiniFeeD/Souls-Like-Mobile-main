using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Inventory.InventoryWithSlots.Equipped;
using Inventory.Item.EquippedItem.Weapon;
using PlayerLogic.Animation;
using PlayerLogic.States.State;
using PlayerLogic.States.StateMachine;
using PlayerLogic.States.Transition;
using PlayerLogic.Stats;
using UnityEngine;

public class AttackState : PlayerState, IAttackState, IDisposable
{
    private readonly PlayerStateMachine _playerStateMachine;
    private readonly InventoryEquipped _equippedInventory;
    private readonly IInputService _inputService;
    private readonly PlayerStats _playerStats;

    private WeaponItem _leftWeapon => _equippedInventory.WeaponSlot.LeftHandWeapon;
    private WeaponItem _rightWeapon => _equippedInventory.WeaponSlot.RightHandWeapon;
    private WeaponItem _twoHandWeapon => _equippedInventory.WeaponSlot.TwoHandWeaponItem;

    private WeaponPrefab _attackingWeaponPrefab;
    private WeaponItem _attackingWeapon;
    
    private bool _isClickedLeftAttackButton;
    private bool _isClickedRightAttackButton;
    private bool _isClickedTwoAttackButton;

    private int _leftWeaponCounterCombo;
    private int _rightWeaponCounterCombo;
    private int _twoHandWeaponCounterCombo;
    private int _currentNumberAttack;
    private PlayerStateAnimator _animator;
    private IMoveModule _moveModule;
    private readonly AudioSource _audioSource;
    private readonly PlayerCamera _playerCamera;
    private readonly Transform _transform;


    public AttackState(List<ITransition> transitions,
        PlayerStateMachine playerStateMachine,
        InventoryEquipped equippedInventory,
        IInputService inputService,
        PlayerStats playerStats,
        PlayerStateAnimator animator,
        IMoveModule moveModule,
        AudioSource audioSource,
        PlayerCamera playerCamera,
        Transform transform) : base(transitions)
    {
        _playerStateMachine = playerStateMachine;
        _equippedInventory = equippedInventory;
        _inputService = inputService;
        _playerStats = playerStats;
        _animator = animator;
        _moveModule = moveModule;
        _audioSource = audioSource;
        _playerCamera = playerCamera;
        _transform = transform;

        _inputService.MainAttackButtonUp += OnMainAttackButtonUp;
        _inputService.LeftHandAttackButtonUp += OnLeftHandAttackButtonUp;
        _inputService.RightHandAttackButtonUp += OnRightHandAttackButtonUp;

        _animator.ExitLeftHandAttack += OnExitLeftWeaponAttack;
        _animator.EnableWeaponCollider += OnEnableWeaponCollider;
        _animator.ExitRightHandAttack += OnExitRightWeaponAttack;
        _animator.ExitTwoHandAttack += OnExitTwoWeaponAttack;
    }

    private void OnEnableWeaponCollider() => 
        EnableWeaponCollider();

    private void OnRightHandAttackButtonUp() => 
        _isClickedRightAttackButton = true;

    private void OnLeftHandAttackButtonUp() => 
        _isClickedLeftAttackButton = true;

    private void OnMainAttackButtonUp() => 
        _isClickedTwoAttackButton = true;

    public void Enter(IAttackStatePayloaded payloaded)
    {
        base.Enter();
        switch (payloaded.ButtonType)
        {
            case ButtonType.MainButton:
                Attack(_twoHandWeapon, 0, ref _isClickedTwoAttackButton);
                break;
            case ButtonType.LeftButton:
                Attack(_twoHandWeapon, 0, ref _isClickedTwoAttackButton);
                break;
            case ButtonType.RightButton:
                Attack(_twoHandWeapon, 0, ref _isClickedTwoAttackButton);
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();
        DisableWeaponCollider();
    }


    private void OnExitLeftWeaponAttack()
    {
        ComboAttack(ref _isClickedLeftAttackButton,
            ref _leftWeaponCounterCombo,
            _leftWeapon,
            LocationWeaponInHandType.LeftHand);
    }

    private void OnExitRightWeaponAttack()
    {
        ComboAttack(ref _isClickedRightAttackButton,
            ref _rightWeaponCounterCombo,
            _rightWeapon,
            LocationWeaponInHandType.RightHand);
    }

    private void OnExitTwoWeaponAttack()
    {
        ComboAttack(ref _isClickedTwoAttackButton, 
            ref _twoHandWeaponCounterCombo,
            _twoHandWeapon,
            LocationWeaponInHandType.TwoHand);
    }

    private void ComboAttack(ref bool isClickedAttackButton,
        ref int counterCombo,
        WeaponItem weapon,
        LocationWeaponInHandType locationWeaponInHand)
    {
        DisableWeaponCollider();
        counterCombo++;
        if (isClickedAttackButton && counterCombo + 1 <= weapon.Info.Attack.Count)
            Attack(weapon, counterCombo, ref isClickedAttackButton);
        else
            EndAttack(locationWeaponInHand);
    }

    private void Attack(WeaponItem weaponItem,
        int counterCombo,
        ref bool isClickedAttackButton)
    {
        if (TrySubtractStamina(weaponItem, counterCombo) == false)
        {
            _playerStateMachine.Enter<IdleState>();
            return;
        }

        _attackingWeapon = weaponItem;
        _currentNumberAttack = counterCombo;
        _attackingWeaponPrefab = weaponItem.SpawnedPrefab.GetComponent<WeaponPrefab>();
        
        weaponItem.Attack[counterCombo].Enter();
        PlayAudioClip(weaponItem, counterCombo);

        _animator.StartAttackAnimation(counterCombo);
        isClickedAttackButton = false;
        
        Rotate();
        Move(weaponItem, counterCombo);
    }

    private bool TrySubtractStamina(WeaponItem weaponItem, int numberAttack) => 
        _playerStats.Stamina.TryUse(weaponItem.Info.Attack[numberAttack].StaminaCost);

    private void Move(WeaponItem weaponItem, int counterCombo)
    {
        _moveModule.MoveAlongACurveUsingCharacterController(
            _transform.forward,
            weaponItem.Info.Attack[counterCombo].Duration,
            1,
            weaponItem.Info.Attack[counterCombo].Curve);
    }

    private void Rotate()
    {
        if (_playerCamera.IsLockedTarget == false)
            _moveModule.Rotate(_inputService.Axis);
        else
            _moveModule.Rotate(_playerCamera.CurrentTarget.LockOnTransform);
    }

    private void EnableWeaponCollider()
    {
        _attackingWeaponPrefab.EnableCollider();
        _attackingWeaponPrefab.CollidedWithEnemy += HandlerMakeDamage;
    }

    private void DisableWeaponCollider() => 
        _attackingWeaponPrefab.DisableCollider();

    private void PlayAudioClip(WeaponItem weaponItem, int numberAttack)
    {
        _audioSource.clip = weaponItem.Info.Attack[numberAttack].AttackAudioClip;
        _audioSource.volume = 1;
        _audioSource.PlayDelayed(0.2f);
    }

    private void EndAttack(LocationWeaponInHandType locationWeaponInHandType)
    {
        switch (locationWeaponInHandType)
        {
            case LocationWeaponInHandType.LeftHand:
                FinishAttack(ref _leftWeaponCounterCombo, ref _isClickedLeftAttackButton);
                break;
            case LocationWeaponInHandType.RightHand:
                FinishAttack(ref _rightWeaponCounterCombo, ref _isClickedRightAttackButton);
                break;
            case LocationWeaponInHandType.TwoHand:
                FinishAttack(ref _twoHandWeaponCounterCombo, ref _isClickedTwoAttackButton);
                break;
        }
    }

    private void FinishAttack(ref int counterCombo, ref bool isClickedButton)
    {
        _attackingWeaponPrefab.DisableCollider();
        _attackingWeaponPrefab.CollidedWithEnemy -= HandlerMakeDamage;
        
        counterCombo = 0;
        isClickedButton = false;
        
        _animator.SetComboNumber(counterCombo);
        _animator.StopAttackAnimation();

        _playerStateMachine.Enter<IdleState>();
    }

    private void HandlerMakeDamage(IDamageable damageable)
    {
        var damagePlayer = _playerStats.Damage.Value;
        var damageWeapon = _attackingWeapon.Info.Attack[_currentNumberAttack].Damage;
        
        damageable.ApplyDamage(damagePlayer + damageWeapon);
    }


    public void Dispose()
    {
        _inputService.MainAttackButtonUp -= OnMainAttackButtonUp;
        _inputService.LeftHandAttackButtonUp -= OnLeftHandAttackButtonUp;
        _inputService.RightHandAttackButtonUp-= OnRightHandAttackButtonUp;

        
        _animator.ExitLeftHandAttack -= OnExitLeftWeaponAttack;
        _animator.ExitRightHandAttack -= OnExitRightWeaponAttack;
        _animator.EnableWeaponCollider -= OnEnableWeaponCollider;
        _animator.ExitTwoHandAttack -= OnExitTwoWeaponAttack;
    }

}