using System;
using UnityEngine;

public class EquippedSlot : IEquippedSlot
{
    public event Action<EquippedEventInfo> EquippedItem;
    public event Action<EquippedEventInfo> UnequippedItem;

    public EquippedItem Item { get; private set; }
    public EquippedItemType Type => _equippedSlotType;

    private EquippedItemType _equippedSlotType;
    private readonly EquippedItem _basedItem;


    public EquippedSlot(EquippedItemType equippedSlotType, EquippedItem basedItem)
    {
        _equippedSlotType = equippedSlotType;
        _basedItem = basedItem;
        Equip(basedItem, true);
    }
    public EquippedSlot(EquippedItemType equippedSlotType)
    {
        _equippedSlotType = equippedSlotType;
    }

    public bool TryEquip(EquippedItem item)
    {
        if (item.Info.Type != _equippedSlotType) 
            return false;
        
        Equip(item, false);
        return true;
    }

    public bool TryUnequip()
    {
        Clear();
        Equip(_basedItem, true);
        return true;
    }

    private void Clear()
    {
        Item.State.UnEquipped();
        UnequippedItem?.Invoke(new EquippedEventInfo(Item.Info, false));
        Item = null;
    }

    private void Equip(EquippedItem newEquippedItem, bool isBasedItem)
    {
        Item = newEquippedItem;
        Item.State.Equipped();
        EquippedItem?.Invoke(new EquippedEventInfo(Item.Info, isBasedItem));
    }
}
