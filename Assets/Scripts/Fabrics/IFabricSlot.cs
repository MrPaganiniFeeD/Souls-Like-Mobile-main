using System.Collections.Generic;

namespace Fabrics
{
    public interface IFabricSlot
    {
        WeaponSlot<WeaponItem> WeaponSlot { get; }

        IInventorySlot CreateInventorySlot();
        IInventorySlot CreateInventorySlot(Item item);
        
        List<IEquippedSlot<EquippedItem>> CreateEquippedSlot();
    }
}