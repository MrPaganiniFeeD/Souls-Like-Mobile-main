using System;
using Data;
using Fabrics;
using Inventory.InventoryWithSlots.Equipped;
using Hero.Stats;
using UnityEngine;

namespace Infrastructure.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly IItemDataBaseService _itemDataBaseService;
        public InventoryEquipped InventoryEquipped { get; }
        public InventoryWithSlots InventoryWithSlots { get; }

        public InventoryService(IItemDataBaseService itemDataBaseService, PlayerStats playerStats)
        {
            _itemDataBaseService = itemDataBaseService;
            IFabricSlot fabricSlot = new FabricSlot(playerStats, itemDataBaseService);
            InventoryEquipped = new InventoryEquipped(fabricSlot);
            InventoryWithSlots = new InventoryWithSlots(fabricSlot);

            
            LoadDefaultItems();
        }

        private void LoadDefaultItems()
        {
            var ironBoots = _itemDataBaseService.GetItem<EquippedInfo>("IronBoots").GetCreationItem();
            var ironArmor = _itemDataBaseService.GetItem<EquippedInfo>("IronArmor").GetCreationItem();
            var ironHamlet = _itemDataBaseService.GetItem<EquippedInfo>("IronHamlet").GetCreationItem();
            var swordRapier =  _itemDataBaseService.GetItem<WeaponInfo>("SwordRapier").GetCreationItem();

            InventoryWithSlots.TryToAdd(ironArmor);
            InventoryWithSlots.TryToAdd(ironBoots);
            InventoryWithSlots.TryToAdd(ironHamlet);
            InventoryWithSlots.TryToAdd(swordRapier);
        }

        public void LoadProgress(PlayerProgress playerProgress) =>
            throw new NotImplementedException();

        public void UpdateProgress(PlayerProgress playerProgress) => 
            throw new NotImplementedException();
        
    }
}