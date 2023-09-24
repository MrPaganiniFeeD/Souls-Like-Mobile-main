using System;
using Inventory.Item.EquippedItem.Weapon;

public class ArmedRightHandState : WeaponSlotState
{
    public override event Action<WeaponItem, LocationWeaponInHandType> WeaponEquipped;
    public override event Action<WeaponItem, LocationWeaponInHandType> WeaponUnequipped;


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
        WeaponEquipped?.Invoke(RightHandWeapon, LocationWeaponInHandType.RightHand);
        WeaponEquipped?.Invoke(LeftHandWeapon, LocationWeaponInHandType.LeftHand);
    }

    protected override void EquipLeftHand(WeaponItem leftHandWeapon) =>
        StateMachine.Enter<ArmedLeftRightHandState>(leftHandWeapon,
            RightHandWeapon,
            null);

    protected override void EquipRightHand(WeaponItem newRightHandWeapon)
    {
        WeaponUnequipped?.Invoke(RightHandWeapon, LocationWeaponInHandType.LeftHand);
        WeaponEquipped?.Invoke(newRightHandWeapon, LocationWeaponInHandType.RightHand);
        RightHandWeapon = newRightHandWeapon;
    }
    
    public override void Unequip(LocationWeaponInHandType locationWeaponInHandType)
    {
        WeaponUnequipped?.Invoke(RightHandWeapon, LocationWeaponInHandType.RightHand);
        ClearStats(RightHandWeapon.Info);
        StateMachine.Enter<UnarmedState>(LeftHandWeapon,
            RightHandWeapon,
            TwoHandWeapon);
    }
}

