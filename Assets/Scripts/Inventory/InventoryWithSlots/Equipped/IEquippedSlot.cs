using System;

public interface IEquippedSlot
{
    public event Action<EquippedEventInfo> EquippedItem;
    public event Action<EquippedEventInfo> UnequippedItem;

    new EquippedItem Item { get; }
    EquippedItemType Type { get; }

    bool TryEquip(EquippedItem item);
    bool TryUnequip();
}

public class EquippedEventInfo
{
    public readonly IEquippedItemInfo ItemInfo;
    public readonly bool IsDefaultItem;

    public EquippedEventInfo(IEquippedItemInfo itemInfo, bool isDefaultItem = false)
    {
        ItemInfo = itemInfo;
        IsDefaultItem = isDefaultItem;
    }
}

public interface ISlot
{
    Item Item { get; }
}

