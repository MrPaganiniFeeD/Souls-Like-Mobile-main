using System;

public interface IInventorySlot : ISlot
{
    event Action<Item> InstalledItem;
    event Action<Item> UninstalledItem;


    int ItemID { get; }
    Item Item { get; }
    int Amount { get; }
    int Capacity { get; }
    bool IsFull { get; }
    bool IsEmpty { get; }
    bool ItemIsEquipped { get; }

    void SetItem(Item item);
    void AddItem(int count);
    void Clear();
    
}
