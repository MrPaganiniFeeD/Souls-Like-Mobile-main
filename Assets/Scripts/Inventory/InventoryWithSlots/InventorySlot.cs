using System;
using Inventory.InventoryWithSlots.Equipped;
using UnityEngine;

public class InventorySlot : IInventorySlot
{
    public event Action<Item> InstalledItem;
    public event Action<Item> UninstalledItem;

    public int ItemID => Item.Info.ID;
    public Item Item { get; private set; }

    public int Capacity { get; private set; }
    public int Amount => Item.State.Amount;


    public bool ItemIsEquipped => Item.State.IsEquipped;
    public bool IsFull => Amount == Capacity;
    public bool IsEmpty => Item == null; 

    private readonly InventoryEquipped _inventoryEquipped;

    public InventorySlot()
    {
    }
    public InventorySlot(Item item)
    {
        SetItem(item);
    }
    
    public void SetItem(Item item)
    {   
        Item = item;
        Capacity = item.Info.MaxItemsInInventorySlot;
        InstalledItem?.Invoke(Item);   
    }

    public bool TryEquippedItem()
    {
        if (Item is EquippedItem equippedItem)
            return _inventoryEquipped.TryEquipped(equippedItem);

        return false;
    }

    public bool TryUnequippedItem()
    {
        if (Item is EquippedItem equippedItem)
            return _inventoryEquipped.TryUnequip(equippedItem);
        return false;
    }
    public void AddItem(int count)
    {
        Item.State.Amount += count;
        InstalledItem?.Invoke(Item);
    }
    public void Clear()
    {
        Item.State.Amount = 0;
        Item = null;
        UninstalledItem?.Invoke(Item);
    }
    
}
