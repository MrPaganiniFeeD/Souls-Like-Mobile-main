using System;
using Inventory.Item.EquippedItem.Weapon;

public class ArmedLeftHandState : WeaponSlotState
{
    public override event Action<WeaponEventInfo> WeaponEquipped;
    public override event Action<WeaponEventInfo> WeaponUnequipped;

    public ArmedLeftHandState(IWeaponSlotStateMachine stateMachine, 
        ApplyingItemStats applyingItemStats,
        WeaponItem unarmedLeft,
        WeaponItem unarmedRight,
        WeaponItem unarmedTwoHand)
        : base(stateMachine, applyingItemStats, unarmedLeft, unarmedRight, unarmedTwoHand)
    {
    }

    public override void Enter(WeaponItem leftHandWeaponItem,
        WeaponItem rightHandWeapon,
        WeaponItem twoHandWeapon)
    {
        base.Enter(leftHandWeaponItem, rightHandWeapon, twoHandWeapon);
        LeftHandWeapon = leftHandWeaponItem;
        RightHandWeapon = UnarmedRight;
        TwoHandWeapon = null;
        WeaponEquipped?.Invoke(new WeaponEventInfo(LeftHandWeapon, LocationWeaponInHandType.LeftHand, false));
        WeaponEquipped?.Invoke(new WeaponEventInfo(RightHandWeapon, LocationWeaponInHandType.RightHand, true));
    }

    protected override void EquipRightHand(WeaponItem rightHandWeapon) =>
        StateMachine.Enter<ArmedLeftRightHandState>(LeftHandWeapon,
            rightHandWeapon,
            null);

    protected override void EquipLeftHand(WeaponItem newLeftHandWeapon)
    {
        ClearSlot(new WeaponEventInfo(LeftHandWeapon, LocationWeaponInHandType.LeftHand, false));
        WeaponEquipped?.Invoke(new WeaponEventInfo(newLeftHandWeapon, LocationWeaponInHandType.LeftHand, false));
        LeftHandWeapon = newLeftHandWeapon;
    }

    public override void Unequip(LocationWeaponInHandType locationWeaponInHandType)
    {
        ClearSlot(new WeaponEventInfo(LeftHandWeapon, LocationWeaponInHandType.LeftHand, false));
        LeftHandWeapon.State.UnEquipped();
        StateMachine.Enter<UnarmedState>(LeftHandWeapon,
            RightHandWeapon,
            TwoHandWeapon);
    }
}