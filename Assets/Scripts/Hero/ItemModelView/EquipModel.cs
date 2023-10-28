using System;
using Infrastructure.Services.Inventory;
using Inventory.InventoryWithSlots.Equipped;
using UnityEngine;
using Zenject;

public class EquipModel : MonoBehaviour, IDisposable
{
    [SerializeField] private BootsModelChanger _bootsModelChanger; 
    [SerializeField] private HamletModelChanger _hamletModelChanger;
    [SerializeField] private ArmorModelChanger _armorModelChanger;
    private InventoryEquipped _inventory;

    [Inject]
    public void Construct(IInventoryService inventoryService)
    {
        _inventory = inventoryService.InventoryEquipped;
    }

    private void Start()
    {
        AllItemEquip();
        foreach (var slot in _inventory.Slots)
        {
            slot.EquippedItem += Equip;
            slot.UnequippedItem += UnEquip;
        }
    }

    private void AllItemEquip()
    {
        
        foreach (var var in _inventory.Slots)
        {
            if (var.Item != null)
                Equip(new EquippedEventInfo(var.Item.Info));
        }
    }

    private void Equip(EquippedEventInfo eventInfo)
    {
        var itemInfo = eventInfo.ItemInfo;
        switch (itemInfo.Type)
        {
            case EquippedItemType.Boots:
                _bootsModelChanger.EquipModelByNames(itemInfo.ModelInfo.Names, itemInfo.ModelInfo.IsNacktModelUnload);
                break;
            case EquippedItemType.Hamlet:
                _hamletModelChanger.EquipModelByNames(itemInfo.ModelInfo.Names, itemInfo.ModelInfo.IsNacktModelUnload);
                break;
            case EquippedItemType.Armor:
                _armorModelChanger.EquipModelByNames(itemInfo.ModelInfo.Names, itemInfo.ModelInfo.IsNacktModelUnload);
                break;

        }
    }
    public void UnEquip(EquippedEventInfo eventInfo)
    {
        var itemInfo = eventInfo.ItemInfo;
        EquippedItemType equippedItemType = itemInfo.Type;
        switch (equippedItemType)
        {
            case EquippedItemType.Boots:
                _bootsModelChanger.EnableDefaultModel();
                break;
            case EquippedItemType.Hamlet:
                _hamletModelChanger.EnableDefaultModel();
                break;
            case EquippedItemType.Armor:
                _armorModelChanger.EnableDefaultModel();
                break;
        }
    }


    public void Dispose()
    {
        foreach (var slot in _inventory.Slots)
        {
            slot.EquippedItem -= Equip;
            slot.UnequippedItem -= UnEquip;
        }
    }
}

