using System.Collections.Generic;
using Animation;
using Inventory.Item.EquippedItem.Weapon;
using Inventory.Item.EquippedItem.Weapon.Attack;
using UnityEngine;
using AnimationInfo = Animation.AnimationInfo;


[CreateAssetMenu(fileName = "New Item Info", menuName = "Items/Equipped/Weapon/Create New Weapon", order = 51)]

public class WeaponInfo  : EquippedInfo, IWeaponInfo
{
    [Header("Attack Data")] 
    [SerializeField] private List<AttackData> _attacksInfo;

    [Header("WeaponType")]
    [SerializeField] private LocationWeaponInHandType _locationWeaponInHandType;
    
    [Header("Animation Info")]
    [SerializeField] private AnimationInfo _animationInfo;

    [Header("Audio Info")]
    [SerializeField] private AudioClip _attackAudioClip;

    public AudioClip AttackAudioClip => _attackAudioClip;

    public IAnimationInfo AnimationInfo => _animationInfo;

    public List<AttackData> AttackInfos => _attacksInfo;

    public LocationWeaponInHandType LocationWeaponInHandType => _locationWeaponInHandType;

    public new WeaponItem GetCreationItem() =>
        new WeaponItem(this);
}