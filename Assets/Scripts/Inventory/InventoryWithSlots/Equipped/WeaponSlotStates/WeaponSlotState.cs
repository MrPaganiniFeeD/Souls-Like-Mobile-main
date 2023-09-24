using System;
using Inventory.Item.EquippedItem.Weapon;

public abstract class WeaponSlotState
{
    public virtual event Action<WeaponItem, LocationWeaponInHandType> WeaponEquipped;
    public virtual event Action<WeaponItem, LocationWeaponInHandType> WeaponUnequipped;
    
    public WeaponItem LeftHandWeapon { get; protected set; }
    public WeaponItem RightHandWeapon { get; protected set; }
    public WeaponItem TwoHandWeapon { get; protected set; }
    
    
    protected readonly IWeaponSlotStateMachine StateMachine;

    protected readonly WeaponItem UnarmedLeft;
    protected readonly WeaponItem UnarmedRight;
    protected readonly WeaponItem UnarmedTwoHand;
    private ApplyingItemStats _applyingItemStats;

    public WeaponSlotState(IWeaponSlotStateMachine stateMachine,
        ApplyingItemStats applyingItemStats,
        WeaponItem unarmedLeft,
        WeaponItem unarmedRight,
        WeaponItem unarmedTwoHand)
    {
        StateMachine = stateMachine;
        _applyingItemStats = applyingItemStats;
        UnarmedLeft = unarmedLeft;
        UnarmedRight = unarmedRight;
        UnarmedTwoHand = unarmedTwoHand;
        
        CreateAttack(UnarmedLeft);
        CreateAttack(UnarmedRight);
        CreateAttack(UnarmedTwoHand);
    }

    public virtual void Enter(WeaponItem leftHandWeaponItem,
        WeaponItem rightHandWeapon,
        WeaponItem twoHandWeapon)
    {
        if (leftHandWeaponItem != null)
            _applyingItemStats.Equip(leftHandWeaponItem.Info.ItemBuffStats, leftHandWeaponItem.Info);
        if (rightHandWeapon != null)
            _applyingItemStats.Equip(rightHandWeapon.Info.ItemBuffStats, rightHandWeapon.Info);
        if (twoHandWeapon != null)
            _applyingItemStats.Equip(twoHandWeapon.Info.ItemBuffStats, twoHandWeapon.Info);
    }

    public bool TryEquip(WeaponItem weapon, LocationWeaponInHandType whichHandInHandType)
    {
        LocationWeaponInHandType inHandTypeLocationWeaponInHand = weapon.Info.LocationWeaponInHandType;
        switch (whichHandInHandType)
        {
            case LocationWeaponInHandType.LeftHand:
                if (TryEquipWeaponInLeftHand(weapon, whichHandInHandType))
                {
                    CreateAttack(weapon);
                    EquipLeftHand(weapon);
                    return true;
                }

                break;
            case LocationWeaponInHandType.RightHand:
                if (TryEquipWeaponInRightHand(weapon, whichHandInHandType))
                {
                    CreateAttack(weapon);
                    EquipRightHand(weapon);
                    return true;
                }

                break;
            case LocationWeaponInHandType.TwoHand:
                if (TryEquipWeaponInTwoHand(weapon))
                {
                    CreateAttack(weapon);
                    EquipTwoHand(weapon);
                    return true;
                }

                break;
        }

        return false;
    }

    private void CreateAttack(WeaponItem weapon)
    {
        if (weapon.Attack.Count == 0)
        {
            foreach (var attackInfo in weapon.Info.AttackInfos)
            {
                switch (attackInfo.AttackType)
                {
                    case AttackType.Melee:
                        weapon.SetNewAttack(new MeleeAttack(attackInfo));
                        break;
                }
            }
        }
    }

    protected virtual void EquipLeftHand(WeaponItem leftHandWeapon) =>
        StateMachine.Enter<ArmedLeftHandState>(leftHandWeapon,
            UnarmedRight,
            null);

    protected virtual void EquipRightHand(WeaponItem rightHandWeapon) =>
        StateMachine.Enter<ArmedRightHandState>(UnarmedLeft,
            rightHandWeapon,
            null);

    protected virtual void EquipTwoHand(WeaponItem weapon) =>
        StateMachine.Enter<ArmedTwoHandState>(null,
            null,
            weapon);

    protected void ClearStats(IWeaponInfo weaponInfo) => 
        _applyingItemStats.UnEquip(weaponInfo);

    public abstract void Unequip(LocationWeaponInHandType locationWeaponInHandType);
    
    

    private bool TryEquipWeaponInLeftHand(WeaponItem equipItem,
        LocationWeaponInHandType whichHand)
    {
        if (GetTryEquipWeaponInHand(equipItem, whichHand) != null)
        {
            LeftHandWeapon = GetTryEquipWeaponInHand(equipItem, whichHand);
            return true;
        }

        return false;
    }

    private bool TryEquipWeaponInRightHand(WeaponItem equipItem,
        LocationWeaponInHandType whichHand)
    {
        if (GetTryEquipWeaponInHand(equipItem, whichHand) != null)
        {
            RightHandWeapon = GetTryEquipWeaponInHand(equipItem, whichHand);
            return true;
        }

        return false;
    }

    private WeaponItem GetTryEquipWeaponInHand(WeaponItem equipItem,
        LocationWeaponInHandType whichHand)
    {
        LocationWeaponInHandType inHandTypeLocationWeaponInHand = equipItem.Info.LocationWeaponInHandType;
        if (inHandTypeLocationWeaponInHand == whichHand ||
            inHandTypeLocationWeaponInHand == LocationWeaponInHandType.AnyHand)
        {
            return equipItem;
        }

        return null;
    }

    private bool TryEquipWeaponInTwoHand(WeaponItem equipItem)
    {
        if (equipItem.Info.LocationWeaponInHandType == LocationWeaponInHandType.TwoHand)
        {
            if (LeftHandWeapon != null)
                WeaponUnequipped?.Invoke(LeftHandWeapon, LocationWeaponInHandType.LeftHand);
            if (RightHandWeapon != null)
                WeaponUnequipped?.Invoke(RightHandWeapon, LocationWeaponInHandType.RightHand);
            TwoHandWeapon = equipItem;
            return true;
        }

        return false;
    }
}