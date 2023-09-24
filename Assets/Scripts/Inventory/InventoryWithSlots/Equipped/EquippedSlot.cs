using System;

public class EquippedSlot<T> : IEquippedSlot<EquippedItem> where T : EquippedItem
{
    public event Action<IEquippedItemInfo> ItemEquipped;
    public event Action<IEquippedItemInfo> ItemUnequipped;


    public EquippedItem Item { get; private set; }
    public EquippedItemType Type => _equippedSlotType;

    private EquippedItemType _equippedSlotType;
    

    public EquippedSlot(EquippedItemType equippedSlotType)
    {
        _equippedSlotType = equippedSlotType;
    }

    public bool TryEquip(EquippedItem item)
    {
        if (item is EquippedItem newEquippedItem && newEquippedItem.Info.Type == _equippedSlotType)
        {
            Item = (T) newEquippedItem;
            ItemEquipped?.Invoke(Item.Info);
            return true;
        }
        return false;
    }

    public bool TryUnequip()
    {
        Clear();
        return true;
    }

    public void Clear()
    {
        Item.State.UnEquipped();
        ItemUnequipped?.Invoke(Item.Info);
        Item = null;
    }
}
