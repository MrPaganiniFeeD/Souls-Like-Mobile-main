using System;
using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;

public class UnarmedState : WeaponSlotState
{
    public override event Action<WeaponItem, LocationWeaponInHandType> WeaponEquipped;
    public override event Action<WeaponItem, LocationWeaponInHandType> WeaponUnequipped;

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
        TwoHandWeapon = twoHandWeapon;
        Debug.Log(TwoHandWeapon);
        WeaponEquipped?.Invoke(UnarmedTwoHand, LocationWeaponInHandType.TwoHand);
    }


    public override void Unequip(LocationWeaponInHandType locationWeaponInHandType) => 
        throw new NotImplementedException();
}