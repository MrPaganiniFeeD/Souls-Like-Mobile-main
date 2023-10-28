using System;
using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;

public class ArmedTwoHandState : WeaponSlotState
{
    public override event Action<WeaponEventInfo> WeaponEquipped;


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
        WeaponEquipped?.Invoke(new WeaponEventInfo(twoHandWeapon, LocationWeaponInHandType.TwoHand, false));
        TwoHandWeapon = twoHandWeapon;
    }

    public override void Unequip(LocationWeaponInHandType locationWeaponInHandType)
    {
        Debug.Log("Unequip " + TwoHandWeapon.Info.Name);
        ClearSlot(new WeaponEventInfo(TwoHandWeapon, LocationWeaponInHandType.TwoHand, false));
        StateMachine.Enter<UnarmedState>(LeftHandWeapon,
            RightHandWeapon,
            TwoHandWeapon);
    }
}