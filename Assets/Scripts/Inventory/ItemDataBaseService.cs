using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Items/Database")]
public class ItemDataBaseService : ScriptableObject, ISerializationCallbackReceiver, IItemDataBaseService
{
    public ItemInfo[] Items;
    private Dictionary<int, ItemInfo> _allItemsWithId = new Dictionary<int, ItemInfo>();
    

    public void OnAfterDeserialize()
    {
        for (var i = 0; i < Items.Length; i++)
        {
            Items[i].SetID(i);
            _allItemsWithId.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize() => 
        _allItemsWithId = new Dictionary<int, ItemInfo>();

    public ItemInfo GetItem(int id) => 
        _allItemsWithId[id];

    public void Debug()
    {
        foreach (var item in _allItemsWithId)
        {
            UnityEngine.Debug.Log(item.Value);
        }
    }
}

public interface IItemDataBaseService : IService
{
    
    ItemInfo GetItem(int id);

    void Debug();
}
