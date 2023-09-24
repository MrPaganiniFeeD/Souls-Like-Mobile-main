using System;
using Inventory.Item.EquippedItem.Weapon;

public class ArmedLeftHandState : WeaponSlotState
{
    public override event Action<WeaponItem, LocationWeaponInHandType> WeaponEquipped;
    public override event Action<WeaponItem, LocationWeaponInHandType> WeaponUnequipped;

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
        WeaponEquipped?.Invoke(LeftHandWeapon, LocationWeaponInHandType.LeftHand);
        WeaponEquipped?.Invoke(RightHandWeapon,LocationWeaponInHandType.RightHand);
    }

    protected override void EquipRightHand(WeaponItem rightHandWeapon) =>
        StateMachine.Enter<ArmedLeftRightHandState>(LeftHandWeapon,
            rightHandWeapon,
            null);

    protected override void EquipLeftHand(WeaponItem newLeftHandWeapon)
    {
        WeaponUnequipped?.Invoke(LeftHandWeapon, LocationWeaponInHandType.LeftHand);
        WeaponEquipped?.Invoke(newLeftHandWeapon,LocationWeaponInHandType.LeftHand);
        LeftHandWeapon = newLeftHandWeapon;
    }

    public override void Unequip(LocationWeaponInHandType locationWeaponInHandType)
    {
        WeaponUnequipped?.Invoke(LeftHandWeapon, LocationWeaponInHandType.LeftHand);
        ClearStats(RightHandWeapon.Info);
        StateMachine.Enter<UnarmedState>(LeftHandWeapon,
            RightHandWeapon,
            TwoHandWeapon);
    }
}