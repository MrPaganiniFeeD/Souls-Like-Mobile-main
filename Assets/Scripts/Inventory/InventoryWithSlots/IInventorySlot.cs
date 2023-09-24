using System;

public interface IInventorySlot
{
    event Action InstalledItem;
    event Action UninstalledItem;


    int ItemID { get; }
    Item Item { get; }
    int Amount { get; }
    int Capacity { get; }
    bool IsFull { get; }
    bool ItemIsEquipped { get; }

    void SetItem(Item item);
    void AddItem(int count);
    void Clear();
    
}
