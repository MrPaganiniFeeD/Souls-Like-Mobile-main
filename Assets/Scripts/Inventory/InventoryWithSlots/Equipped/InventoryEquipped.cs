using System;
using System.Collections.Generic;
using Fabrics;
using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;

namespace Inventory.InventoryWithSlots.Equipped
{
    public class InventoryEquipped : IInventory
    {
        public List<IEquippedSlot> Slots { get; private set; }
        public WeaponSlot WeaponSlot => _weaponSlot;

        private IFabricSlot _fabricSlot;
        private WeaponSlot _weaponSlot;

        public InventoryEquipped(IFabricSlot fabricSlot)
        {
            _fabricSlot = fabricSlot;
            Slots = _fabricSlot.CreateEquippedSlot();
            _weaponSlot = _fabricSlot.WeaponSlot;
            
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
    }
}
