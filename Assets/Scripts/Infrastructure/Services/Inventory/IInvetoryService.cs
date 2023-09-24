using Infrastructure.Services.PersistentProgress;
using Inventory.InventoryWithSlots.Equipped;

namespace Infrastructure.Services.Inventory
{
    public interface IInventoryService : IService, ISavedProgress
    {
        InventoryEquipped InventoryEquipped { get; }
        InventoryWithSlots InventoryWithSlots { get; }
    }
}