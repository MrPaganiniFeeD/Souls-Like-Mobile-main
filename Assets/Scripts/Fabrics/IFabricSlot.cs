using System.Collections.Generic;
using Inventory.InventoryWithSlots.Equipped;

namespace Fabrics
{
    public interface IFabricSlot
    {
        WeaponSlot WeaponSlot { get; }

        IInventorySlot CreateInventorySlot();
        IInventorySlot CreateInventorySlot(Item item);
        
        List<IEquippedSlot> CreateEquippedSlot();
    }
}