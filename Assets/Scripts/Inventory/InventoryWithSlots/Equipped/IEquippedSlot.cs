using System;

public interface IEquippedSlot<T> where T : EquippedItem
{
    public event Action<IEquippedItemInfo> ItemEquipped;
    public event Action<IEquippedItemInfo> ItemUnequipped;

    T Item { get; }
    EquippedItemType Type { get; }

    bool TryEquip(EquippedItem item);
    bool TryUnequip();
}