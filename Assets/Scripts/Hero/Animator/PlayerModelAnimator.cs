using Infrastructure.Services.Inventory;
using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;
using Zenject;

public class PlayerModelAnimator : MonoBehaviour
{
    private Animator _animator;
    private WeaponSlot _weaponSlot;

    [Inject]
    public void Construct(IInventoryService inventoryService)
    {
        _weaponSlot = inventoryService.InventoryEquipped.WeaponSlot;
        _weaponSlot.WeaponEquipped += OnWeaponEquipped;
        _weaponSlot.WeaponUnequipped += OnWeaponUnequipped;
    }

    private void Awake() => 
        _animator = GetComponent<Animator>();

    private void Start()
    {
        OnWeaponEquipped(new WeaponEventInfo(_weaponSlot.LeftHandWeapon, LocationWeaponInHandType.LeftHand, false));   
        OnWeaponEquipped(new WeaponEventInfo(_weaponSlot.RightHandWeapon, LocationWeaponInHandType.RightHand, false));
        OnWeaponEquipped(new WeaponEventInfo(_weaponSlot.TwoHandWeaponItem, LocationWeaponInHandType.TwoHand, false));
    }

    private void OnWeaponUnequipped(WeaponEventInfo eventInfo)
    {
        var weapon = eventInfo.WeaponItem;
        var location = eventInfo.LocationWeaponInHandType;
        var isUnarmed = eventInfo.IsUnarmed;
        
        switch (location)
        {
            case LocationWeaponInHandType.LeftHand:
                _animator.SetInteger(NameAnimationParameters.LeftWeapon, 0);
                break;
            case LocationWeaponInHandType.RightHand:
                _animator.SetInteger(NameAnimationParameters.RightWeapon, 0);
                break;
            case LocationWeaponInHandType.TwoHand:
                _animator.SetInteger(NameAnimationParameters.Weapon, 0);
                break;
        }    
        StartIdleAnimation();
    }

    private void OnWeaponEquipped(WeaponEventInfo eventInfo)
    {
        var weapon = eventInfo.WeaponItem;
        if (weapon != null)
            _animator.SetInteger(NameAnimationParameters.Weapon, weapon.Info.AnimationInfo.WeaponNumber);
        StartIdleAnimation();
    }


    public void StartIdleAnimation() =>
        _animator.SetTrigger(NameAnimationParameters.Idle);

    public void StopIdleAnimation() =>
        _animator.ResetTrigger(NameAnimationParameters.Idle);

    private void OnDestroy()
    {
        _weaponSlot.WeaponEquipped -= OnWeaponEquipped;
        _weaponSlot.WeaponUnequipped -= OnWeaponUnequipped;
    }
}
