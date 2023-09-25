using System;
using UnityEngine;

public class EquippedSlot: IEquippedSlot
{
    public event Action<IEquippedItemInfo> ItemEquipped;
    public event Action<IEquippedItemInfo> ItemUnequipped;


    public EquippedItem Item { get; private set; }
    public EquippedItemType Type => _equippedSlotType;

    private EquippedItemType _equippedSlotType;
    private readonly EquippedItem _basedItem;


    public EquippedSlot(EquippedItemType equippedSlotType, EquippedItem basedItem)
    {
        _equippedSlotType = equippedSlotType;
        _basedItem = basedItem;
        Equip(basedItem);
        Debug.Log(Item);
    }
    public EquippedSlot(EquippedItemType equippedSlotType)
    {
        _equippedSlotType = equippedSlotType;
    }

    public bool TryEquip(EquippedItem item)
    {
        if (item.Info.Type != _equippedSlotType) 
            return false;
        
        Equip(item);
        return true;
    }

    public bool TryUnequip()
    {
        Clear();
        Equip(_basedItem);
        return true;
    }

    public void Clear()
    {
        Item.State.UnEquipped();
        ItemUnequipped?.Invoke(Item.Info);
        Item = null;
    }

    private void Equip(EquippedItem newEquippedItem)
    {
        Item = newEquippedItem;
        ItemEquipped?.Invoke(Item.Info);
    }
}
