using System;
using PlayerLogic.Stats;
using UnityEngine;
using Zenject;

namespace Inventory.InventoryWithSlots.Equipped
{
    public class EquipItemStatsHandler : MonoBehaviour
    {
        private InventoryEquipped _inventory;
        private IApplyingItemStats _applyingItemStats;
        
        [Inject]
        public void Constructor(InventoryEquipped inventoryEquipped, PlayerStats playerStats)
        {
            _inventory = inventoryEquipped;
            _applyingItemStats = new ApplyingItemStats(playerStats);
        }
        private void Awake()
        {
            foreach (IInventorySlot inventorySlot in _inventory.Slots)
            {
                IEquippedSlot<EquippedItem> slot = (IEquippedSlot<EquippedItem>) inventorySlot;
                //slot.ItemEquipped += OnEquippedItem;
                //slot.ItemUnequipped += OnUnequippedItem;
            }
        }
        private void OnEquippedItem(IEquippedItemInfo itemInfo)
        {
            _applyingItemStats.Equip(itemInfo.ItemBuffStats, itemInfo);
        }

        private void OnUnequippedItem(IEquippedItemInfo itemInfo)
        {
            _applyingItemStats.UnEquip(itemInfo);
        }
        
    }
}