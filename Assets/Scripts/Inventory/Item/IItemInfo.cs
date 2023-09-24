using UnityEngine;

public interface IItemInfo 
{
    int ID { get; }
    string Name { get; }
    string Discription { get; }
    int MaxItemsInInventorySlot { get; }
    Sprite Icon { get; }
    GameObject Prefab { get; }
}