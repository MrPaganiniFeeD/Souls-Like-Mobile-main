using System;
using Data;
using Fabrics;
using Inventory.InventoryWithSlots.Equipped;
using PlayerLogic.Stats;

namespace Infrastructure.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        public InventoryEquipped InventoryEquipped { get; }
        public InventoryWithSlots InventoryWithSlots { get; }

        public InventoryService(IItemDataBaseService itemDataBaseService, PlayerStats playerStats)
        {
            IFabricSlot fabricSlot = new FabricSlot(playerStats, itemDataBaseService);
            InventoryEquipped = new InventoryEquipped(fabricSlot);
            InventoryWithSlots = new InventoryWithSlots(fabricSlot);
        }

        public void LoadProgress(PlayerProgress playerProgress) =>
            throw new NotImplementedException();

        public void UpdateProgress(PlayerProgress playerProgress) => 
            throw new NotImplementedException();
        
    }
}