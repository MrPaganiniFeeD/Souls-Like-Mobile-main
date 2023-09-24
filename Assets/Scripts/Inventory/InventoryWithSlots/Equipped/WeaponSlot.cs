using System;
using System.Collections.Generic;
using Inventory.Item.EquippedItem.Weapon;
using PlayerLogic.Stats;
using UnityEngine;

public class WeaponSlot<T> : IWeaponSlot<T>, IDisposable, IWeaponSlotStateMachine where T : WeaponItem
{
    public event Action<IEquippedItemInfo> ItemEquipped;
    public event Action<IEquippedItemInfo> ItemUnequipped;

    public event Action<WeaponItem, LocationWeaponInHandType> WeaponEquipped;
    public event Action<WeaponItem, LocationWeaponInHandType> WeaponUnequipped;

    public EquippedItem Item { get; }
    public EquippedItemType Type => EquippedItemType.Weapon;

    public WeaponItem LeftHandWeapon => _currentState.LeftHandWeapon;
    public WeaponItem RightHandWeapon => _currentState.RightHandWeapon;
    public WeaponItem TwoHandWeaponItem => _currentState.TwoHandWeapon;

    private WeaponSlotState _currentState;

    private Dictionary<Type, WeaponSlotState> _allState;
    private ApplyingItemStats _applyingItemStats;

    private readonly WeaponItem _leftUnarmed;
    private readonly WeaponItem _rightUnarmed;
    private readonly WeaponItem _unarmedTwoHand;

    public WeaponSlot(PlayerStats playerStats, WeaponItem leftUnarmed, 
        WeaponItem rightUnarmed,
        WeaponItem unarmedTwoHand)
    {
        _leftUnarmed = leftUnarmed;
        _rightUnarmed = rightUnarmed;
        _unarmedTwoHand = unarmedTwoHand;

        _applyingItemStats = new ApplyingItemStats(playerStats);
        _allState = new Dictionary<Type, WeaponSlotState>
        {
            [typeof(ArmedLeftHandState)] = new ArmedLeftHandState(this,
                _applyingItemStats,
                leftUnarmed,
                rightUnarmed,
                unarmedTwoHand),
            [typeof(ArmedRightHandState)] = new ArmedRightHandState(this,
                _applyingItemStats,
                leftUnarmed,
                rightUnarmed,
                unarmedTwoHand),
            [typeof(ArmedLeftRightHandState)] = new ArmedLeftRightHandState(this,
                _applyingItemStats,
                leftUnarmed,
                rightUnarmed,
                unarmedTwoHand),
            [typeof(ArmedTwoHandState)] = new ArmedTwoHandState(this,
                _applyingItemStats,
                leftUnarmed,
                rightUnarmed,
                unarmedTwoHand),
            [typeof(UnarmedState)] = new UnarmedState(this,
                _applyingItemStats,
                leftUnarmed,
                rightUnarmed,
                unarmedTwoHand)
        };
    }

    public bool TryEquip(EquippedItem item)
    {
        if (item is WeaponItem weaponItem)
            if (_currentState.TryEquip(weaponItem, LocationWeaponInHandType.None))
                return true;

        return false;
    }

    public bool TryEquip(WeaponItem item, LocationWeaponInHandType whichHandInHandType) => 
        _currentState.TryEquip(item, whichHandInHandType);

    public bool TryUnequip(LocationWeaponInHandType whichHandInHandType)
    {
        _currentState.Unequip(whichHandInHandType);
        return true;
    }

    public bool TryUnequip() => 
        TryUnequip(LocationWeaponInHandType.None);

    public void Enter<TState>(WeaponItem leftHandWeapon,
        WeaponItem rightHandWeapon, 
        WeaponItem twoHand) where TState : WeaponSlotState
    {
        WeaponSlotState state = ChangeState<TState>();
        state.Enter(leftHandWeapon, rightHandWeapon, twoHand);
    }

    public WeaponSlot<T> SetFistWeapon()
    {
        Debug.Log("SetFistWeapon");
        Enter<ArmedTwoHandState>(null,
            null,
            _unarmedTwoHand);
        Debug.Log(_currentState.TwoHandWeapon.Info.Name);
        return this;
    }

    private TState ChangeState<TState>() where TState : WeaponSlotState
    {
        TState newState = GetState<TState>();
        if (_currentState != null)
        {
            _currentState.WeaponEquipped -= OnWeaponEquipped;
            _currentState.WeaponUnequipped -= OnWeaponUnequipped;
        }
        
        newState.WeaponEquipped += OnWeaponEquipped;
        newState.WeaponUnequipped += OnWeaponUnequipped;
        _currentState = newState;
        
        return newState;
    }

    private void OnWeaponUnequipped(WeaponItem weapon,
        LocationWeaponInHandType locationWeaponInHandType) => 
        WeaponUnequipped?.Invoke(weapon, locationWeaponInHandType);

    private void OnWeaponEquipped(WeaponItem weapon,
        LocationWeaponInHandType locationWeaponInHandType) =>
        WeaponEquipped?.Invoke(weapon, locationWeaponInHandType);

    private TState GetState<TState>() where TState : WeaponSlotState => 
        _allState[typeof(TState)] as TState;

    public void Dispose()
    {
        _currentState.WeaponEquipped -= OnWeaponEquipped;
        _currentState.WeaponUnequipped -= OnWeaponEquipped;
    }
}