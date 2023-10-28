using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;

public class UIWeaponSlotSystem : MonoBehaviour
{
    [SerializeField] private UIEquippedSlot _leftHandSlot;
    [SerializeField] private UIEquippedSlot _rightHandSlot;
    [SerializeField] private UIEquippedSlot _sheild;
    
    
    private WeaponSlot _weaponSlot;

    public void Construct(WeaponSlot weaponSlot)
    {
        _weaponSlot = weaponSlot;
        
        _weaponSlot.WeaponEquipped += OnWeaponEquipped;
        _weaponSlot.WeaponUnequipped += OnWeaponUnequipped;
    }

    private void OnWeaponUnequipped(WeaponEventInfo eventInfo)
    {
       var weapon = eventInfo.WeaponItem;
       var location = eventInfo.LocationWeaponInHandType;
       var isUnarmed = eventInfo.IsUnarmed;
       if (isUnarmed)
           return;
       
       switch (location)
        {
            case LocationWeaponInHandType.TwoHand:
                _leftHandSlot.OnUnequippedItem(new EquippedEventInfo(weapon.Info, isUnarmed));
                break;
            case LocationWeaponInHandType.LeftHand:
                _leftHandSlot.OnUnequippedItem(new EquippedEventInfo(weapon.Info, isUnarmed));
                break;
            case LocationWeaponInHandType.RightHand:
                _rightHandSlot.OnUnequippedItem(new EquippedEventInfo(weapon.Info, isUnarmed));
                break;
        }   
    }

    private void OnWeaponEquipped(WeaponEventInfo eventInfo)
    { 
        var weapon = eventInfo.WeaponItem;
        var location = eventInfo.LocationWeaponInHandType;
        var isUnarmed = eventInfo.IsUnarmed;
        if (isUnarmed)
            return;
        switch (location)
        {
            case LocationWeaponInHandType.TwoHand:
                _leftHandSlot.OnEquippedItem(new EquippedEventInfo(weapon.Info, isUnarmed));
                break;
            case LocationWeaponInHandType.LeftHand:
                _leftHandSlot.OnEquippedItem(new EquippedEventInfo(weapon.Info, isUnarmed));
                break;
            case LocationWeaponInHandType.RightHand:
                _rightHandSlot.OnEquippedItem(new EquippedEventInfo(weapon.Info, isUnarmed));
                break;
        }
    }

    private void OnDestroy()
    {
        _weaponSlot.WeaponUnequipped -= OnWeaponUnequipped;
        _weaponSlot.WeaponEquipped -= OnWeaponEquipped;
    }
}