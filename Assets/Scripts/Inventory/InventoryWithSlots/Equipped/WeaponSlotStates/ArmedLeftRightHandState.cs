using System;
using Inventory.Item.EquippedItem.Weapon;

public class ArmedLeftRightHandState : WeaponSlotState
{
    public override event Action<WeaponItem, LocationWeaponInHandType> WeaponEquipped;
    public override event Action<WeaponItem, LocationWeaponInHandType> WeaponUnequipped;


    public ArmedLeftRightHandState(IWeaponSlotStateMachine stateMachine,
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
        LeftHandWeapon = leftHandWeaponItem;
        RightHandWeapon = rightHandWeapon;
        TwoHandWeapon = null;
        WeaponEquipped?.Invoke(LeftHandWeapon, LocationWeaponInHandType.LeftHand);
        WeaponEquipped?.Invoke(RightHandWeapon,LocationWeaponInHandType.RightHand);
    }

    protected override void EquipLeftHand(WeaponItem newLeftHandWeapon)
    {
        WeaponUnequipped?.Invoke(LeftHandWeapon, LocationWeaponInHandType.LeftHand);
        WeaponEquipped?.Invoke(newLeftHandWeapon,LocationWeaponInHandType.LeftHand);
        LeftHandWeapon = newLeftHandWeapon;
    }

    protected override void EquipRightHand(WeaponItem newRightHandWeapon)
    {
        WeaponUnequipped?.Invoke(RightHandWeapon, LocationWeaponInHandType.RightHand);
        WeaponEquipped?.Invoke(newRightHandWeapon,LocationWeaponInHandType.RightHand);
        RightHandWeapon = newRightHandWeapon;
    }

    public override void Unequip(LocationWeaponInHandType locationWeaponInHandType)
    {
        switch (locationWeaponInHandType)
        {
            case LocationWeaponInHandType.LeftHand:
                WeaponUnequipped?.Invoke(LeftHandWeapon, LocationWeaponInHandType.LeftHand);
                ClearStats(LeftHandWeapon.Info);
                StateMachine.Enter<ArmedRightHandState>(LeftHandWeapon,
                    RightHandWeapon,
                    TwoHandWeapon);
                break;
            case LocationWeaponInHandType.RightHand:
                WeaponUnequipped?.Invoke(RightHandWeapon, LocationWeaponInHandType.RightHand);
                ClearStats(RightHandWeapon.Info);
                StateMachine.Enter<ArmedLeftHandState>(LeftHandWeapon,
                    RightHandWeapon,
                    TwoHandWeapon);
                break;
        }
    }
}