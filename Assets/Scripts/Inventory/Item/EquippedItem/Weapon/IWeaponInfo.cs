using System.Collections.Generic;
using Animation;
using Inventory.Item.EquippedItem.Weapon;
using Inventory.Item.EquippedItem.Weapon.Attack;
using UnityEngine;

public interface IWeaponInfo : IEquippedItemInfo
{
    GameObject Prefab { get; }
    IAnimationInfo AnimationInfo { get; }
    List<AttackData> AttackInfos { get; }
    LocationWeaponInHandType LocationWeaponInHandType { get; }
}
