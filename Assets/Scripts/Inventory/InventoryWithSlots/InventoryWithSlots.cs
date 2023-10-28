using System;
using System.Collections.Generic;
using Fabrics;
using UnityEngine;


public class InventoryWithSlots : IInventory
{
    public event Action StateChanged;
    
    public List<IInventorySlot> Slots { get; private set; }
    private IFabricSlot _fabricSlot;
    private int _capacity = 50;


    public InventoryWithSlots(IFabricSlot fabricSlot)
    {
        _fabricSlot = fabricSlot;
        Slots = new List<IInventorySlot>();
        for (int i = 0; i < _capacity; i++)
        {
            Slots.Add(_fabricSlot.CreateInventorySlot());
        }
    }

    public bool TryToAdd(Item item)
    {
        if (item.Info.MaxItemsInInventorySlot == 1)
        {
            foreach (var slot in Slots)
            {
                if (slot.IsEmpty)
                {
                    slot.SetItem(item);
                    StateChanged?.Invoke();
                    return true;
                }
            }
        }
        else
        {
            foreach (var slot in Slots)
            {
                if (item.Info.ID == slot.ItemID && slot.IsFull == false)
                {
                    AddItemToSlot(slot, item, item.State.Amount);
                    StateChanged?.Invoke();
                    return true;
                }
            }

            /*var clonedItem = item.Clone();
            CreateNewSlots(clonedItem);
            StateChanged?.Invoke();
            return true;*/
        }

        return false;
    }
    
    private void AddItemToSlot(IInventorySlot slot, Item item, int count)
    {
        int countCanAdd = slot.Capacity - slot.Amount;
        if (count <= countCanAdd)
        {
            slot.AddItem(count);
        }
        else
        {
            int remainder = count - countCanAdd;
            Item clonedItem = item.Clone();
            clonedItem.State.Amount = remainder;
            TryToAdd(clonedItem);
        }
    }
    private IInventorySlot CreateNewSlots(Item item)
    {
        IInventorySlot newSlot = _fabricSlot.CreateInventorySlot(item);  
        Debug.Log(Slots);
        Slots.Add(newSlot);
        return newSlot;
    }
}
