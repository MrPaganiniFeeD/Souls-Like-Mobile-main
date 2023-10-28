using System;
using System.Collections.Generic;
using Inventory.Item.EquippedItem.Weapon;
using Hero.Stats;
using UnityEngine;

public class WeaponSlot : IWeaponSlot, IDisposable, IWeaponSlotStateMachine 
{
    public event Action<EquippedEventInfo> EquippedItem;
    public event Action<EquippedEventInfo> UnequippedItem;

    public event Action<WeaponEventInfo> WeaponEquipped;
    public event Action<WeaponEventInfo> WeaponUnequipped;
    
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
            {
                item.State.Equipped();
                return true;
            }
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

    public WeaponSlot SetFistWeapon()
    {
        Debug.Log("SetFistWeapon");
        Enter<UnarmedState>(null,
            null,
            _unarmedTwoHand);
        return this;
    }

    private TState ChangeState<TState>() where TState : WeaponSlotState
    {
        TState newState = GetState<TState>();
        if (_currentState != null)
        {
            _currentState.Exit();
            _currentState.WeaponEquipped -= OnWeaponEquipped;
            _currentState.WeaponUnequipped -= OnWeaponUnequipped;
        }
        
        newState.WeaponEquipped += OnWeaponEquipped;
        newState.WeaponUnequipped += OnWeaponUnequipped;
        _currentState = newState;
        
        return newState;
    }

    private void OnWeaponUnequipped(WeaponEventInfo weaponEventInfo) => 
        WeaponUnequipped?.Invoke(weaponEventInfo);

    private void OnWeaponEquipped(WeaponEventInfo weaponEventInfo) =>
        WeaponEquipped?.Invoke(weaponEventInfo);

    private TState GetState<TState>() where TState : WeaponSlotState => 
        _allState[typeof(TState)] as TState;

    public void Dispose()
    {
        _currentState.WeaponEquipped -= OnWeaponEquipped;
        _currentState.WeaponUnequipped -= OnWeaponUnequipped;
    }
}

public class WeaponEventInfo
{
    public WeaponItem WeaponItem;
    public LocationWeaponInHandType LocationWeaponInHandType;
    public bool IsUnarmed;

    public WeaponEventInfo(WeaponItem weaponItem, LocationWeaponInHandType locationWeaponInHandType, bool isUnarmed)
    {
        WeaponItem = weaponItem;
        LocationWeaponInHandType = locationWeaponInHandType;
        IsUnarmed = isUnarmed;
    }
}