using System;
using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;

public class ArmedTwoHandState : WeaponSlotState
{
    public override event Action<WeaponItem, LocationWeaponInHandType> WeaponEquipped;
    public override event Action<WeaponItem, LocationWeaponInHandType> WeaponUnequipped;


    public ArmedTwoHandState(IWeaponSlotStateMachine stateMachine,
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
        WeaponEquipped?.Invoke(twoHandWeapon, LocationWeaponInHandType.TwoHand);
        TwoHandWeapon = twoHandWeapon;
    }

    public override void Unequip(LocationWeaponInHandType locationWeaponInHandType)
    {
        ClearStats(TwoHandWeapon.Info);
        WeaponUnequipped?.Invoke(TwoHandWeapon, LocationWeaponInHandType.TwoHand);
        StateMachine.Enter<UnarmedState>(LeftHandWeapon,
            RightHandWeapon,
            TwoHandWeapon);
    }
}