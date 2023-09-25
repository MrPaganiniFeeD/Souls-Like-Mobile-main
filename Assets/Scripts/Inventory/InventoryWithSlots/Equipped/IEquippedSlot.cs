using System;

public interface IEquippedSlot
{
    public event Action<IEquippedItemInfo> ItemEquipped;
    public event Action<IEquippedItemInfo> ItemUnequipped;

    EquippedItem Item { get; }
    EquippedItemType Type { get; }

    bool TryEquip(EquippedItem item);
    bool TryUnequip();
}