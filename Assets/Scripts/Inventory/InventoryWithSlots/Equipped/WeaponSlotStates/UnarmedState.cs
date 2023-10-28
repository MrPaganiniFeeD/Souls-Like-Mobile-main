using System;
using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;

public class UnarmedState : WeaponSlotState
{
    public override event Action<WeaponEventInfo> WeaponEquipped;
    public override event Action<WeaponEventInfo> WeaponUnequipped;

    public UnarmedState(IWeaponSlotStateMachine stateMachine, 
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
        TwoHandWeapon = UnarmedTwoHand;
        WeaponEquipped?.Invoke(new WeaponEventInfo(UnarmedTwoHand, LocationWeaponInHandType.LeftHand, true));
    }


    public override void Unequip(LocationWeaponInHandType locationWeaponInHandType)
        => new NotImplementedException();

    public override void Exit() => 
        WeaponUnequipped?.Invoke(new WeaponEventInfo(TwoHandWeapon, LocationWeaponInHandType.TwoHand, true));
}