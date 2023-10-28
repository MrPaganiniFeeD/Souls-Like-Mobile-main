using System;
using Inventory.Item.EquippedItem.Weapon;

public class ArmedLeftRightHandState : WeaponSlotState
{
    public override event Action<WeaponEventInfo> WeaponEquipped;


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
        WeaponEquipped?.Invoke(new WeaponEventInfo(LeftHandWeapon, LocationWeaponInHandType.LeftHand, false));
        WeaponEquipped?.Invoke(new WeaponEventInfo(RightHandWeapon, LocationWeaponInHandType.RightHand, false));
    }

    protected override void EquipLeftHand(WeaponItem newLeftHandWeapon)
    {
        ClearSlot(new WeaponEventInfo(LeftHandWeapon, LocationWeaponInHandType.LeftHand, false));
        WeaponEquipped?.Invoke(new WeaponEventInfo(newLeftHandWeapon, LocationWeaponInHandType.LeftHand, false));
        LeftHandWeapon = newLeftHandWeapon;
    }

    protected override void EquipRightHand(WeaponItem newRightHandWeapon)
    {
        ClearSlot(new WeaponEventInfo(RightHandWeapon, LocationWeaponInHandType.RightHand, false));
        WeaponEquipped?.Invoke(new WeaponEventInfo(newRightHandWeapon, LocationWeaponInHandType.RightHand, false));
        RightHandWeapon = newRightHandWeapon;
    }

    public override void Unequip(LocationWeaponInHandType locationWeaponInHandType)
    {
        switch (locationWeaponInHandType)
        {
            case LocationWeaponInHandType.LeftHand:
                ClearSlot(new WeaponEventInfo(LeftHandWeapon, LocationWeaponInHandType.LeftHand, false));
                StateMachine.Enter<ArmedRightHandState>(LeftHandWeapon,
                    RightHandWeapon,
                    TwoHandWeapon);
                break;
            case LocationWeaponInHandType.RightHand:
                ClearSlot(new WeaponEventInfo(RightHandWeapon, LocationWeaponInHandType.RightHand, false));
                StateMachine.Enter<ArmedLeftHandState>(LeftHandWeapon,
                    RightHandWeapon,
                    TwoHandWeapon);
                break;
        }
    }
}