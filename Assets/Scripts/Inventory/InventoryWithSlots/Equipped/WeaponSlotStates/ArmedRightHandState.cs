using System;
using Inventory.Item.EquippedItem.Weapon;

public class ArmedRightHandState : WeaponSlotState
{
    public override event Action<WeaponEventInfo> WeaponEquipped;


    public ArmedRightHandState(IWeaponSlotStateMachine stateMachine, 
        ApplyingItemStats applyingItemStats,
        WeaponItem unarmedLeft,
        WeaponItem unarmedRight,
        WeaponItem unarmedTwoHand)
        : base(stateMachine, applyingItemStats, unarmedLeft, unarmedRight, unarmedTwoHand)
    {
    }

    public override void Enter(WeaponItem leftHandWeaponItem, WeaponItem rightHandWeapon, WeaponItem twoHandWeapon)
    {
        base.Enter(leftHandWeaponItem, rightHandWeapon, twoHandWeapon);
        RightHandWeapon = rightHandWeapon;
        LeftHandWeapon = UnarmedLeft;
        WeaponEquipped?.Invoke(new WeaponEventInfo(RightHandWeapon, LocationWeaponInHandType.RightHand, false));
        WeaponEquipped?.Invoke(new WeaponEventInfo(LeftHandWeapon, LocationWeaponInHandType.RightHand, true));
    }

    protected override void EquipLeftHand(WeaponItem leftHandWeapon) =>
        StateMachine.Enter<ArmedLeftRightHandState>(leftHandWeapon,
            RightHandWeapon,
            null);

    protected override void EquipRightHand(WeaponItem newRightHandWeapon)
    {
        ClearSlot(new WeaponEventInfo(RightHandWeapon, LocationWeaponInHandType.RightHand, false));
        WeaponEquipped?.Invoke(new WeaponEventInfo(newRightHandWeapon, LocationWeaponInHandType.RightHand, false));
        RightHandWeapon = newRightHandWeapon;
    }
    
    public override void Unequip(LocationWeaponInHandType locationWeaponInHandType)
    {
        ClearSlot(new WeaponEventInfo(RightHandWeapon, LocationWeaponInHandType.RightHand, false));
        StateMachine.Enter<UnarmedState>(LeftHandWeapon,
            RightHandWeapon,
            TwoHandWeapon);
    }
}

