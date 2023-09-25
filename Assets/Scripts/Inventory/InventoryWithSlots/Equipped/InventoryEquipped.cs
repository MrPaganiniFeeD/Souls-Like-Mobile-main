using System;
using System.Collections.Generic;
using Fabrics;
using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;

namespace Inventory.InventoryWithSlots.Equipped
{
    public class InventoryEquipped : IDisposable, IInventory
    {
        public event Action<WeaponItem, LocationWeaponInHandType> WeaponEquip;
        public event Action<LocationWeaponInHandType> WeaponUnequip;
        
        public List<IEquippedSlot> Slots { get; private set; }
        public WeaponSlot WeaponSlot => _weaponSlot;

        private IFabricSlot _fabricSlot;
        private WeaponSlot _weaponSlot;

        public InventoryEquipped(IFabricSlot fabricSlot)
        {
            _fabricSlot = fabricSlot;
            Slots = _fabricSlot.CreateEquippedSlot();
            _weaponSlot = _fabricSlot.WeaponSlot;

            _weaponSlot.WeaponEquipped += OnWeaponEquipped;
            _weaponSlot.WeaponUnequipped += OnWeaponUnequipped;
        }

        public T GetEquippedItem<T>() where T : EquippedItem
        {
            foreach (var equippedSlot in Slots)
                if (equippedSlot.Item is T)
                    return (T) equippedSlot.Item;
            return null;
        }
        public bool TryEquipped(EquippedItem item)
        {
            foreach (var slot in Slots)
            {
                if (slot.Type == item.Info.Type)
                {
                    return slot.TryEquip(item);
                }
            }

            return false;
        }

        public bool TryUnequip(EquippedItem item)
        {
            foreach (var slot in Slots)
            {
                if (slot.Type == item.Info.Type)
                {
                    return slot.TryUnequip();
                }
            }

            return false;
        }

        private void OnWeaponEquipped(WeaponItem arg1, LocationWeaponInHandType arg2) => 
            WeaponEquip?.Invoke(arg1, arg2);

        private void OnWeaponUnequipped(WeaponItem arg1, LocationWeaponInHandType arg2) => 
            WeaponUnequip?.Invoke(arg2);

        public void UpdateAllWeapon()
        {
            UpdateWeapon(_weaponSlot.LeftHandWeapon, LocationWeaponInHandType.LeftHand);
            UpdateWeapon(_weaponSlot.RightHandWeapon, LocationWeaponInHandType.RightHand);
            UpdateWeapon(_weaponSlot.TwoHandWeaponItem, LocationWeaponInHandType.TwoHand);
        }

        private void UpdateWeapon(WeaponItem weapon,
            LocationWeaponInHandType locationWeaponInHandType)
        {
            if (weapon != null)
            {
                WeaponEquip?.Invoke(weapon, locationWeaponInHandType);
                Debug.Log(locationWeaponInHandType);
            }
        }

        public void Dispose()
        {
            _weaponSlot.WeaponEquipped -= OnWeaponEquipped;
            _weaponSlot.WeaponUnequipped -= OnWeaponUnequipped;
        }
    }
}
