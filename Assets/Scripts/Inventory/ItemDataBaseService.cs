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

    public T GetItem<T>(string name) where T : ItemInfo
    {
        for (var i = 0; i < Items.Length; i++)
        {
            var item = _allItemsWithId[i];
            if(item.Name == name && item is T info)
            {
                return info;
            }
        }

        return null;
    }
    
}

public interface IItemDataBaseService : IService
{
    
    ItemInfo GetItem(int id);
    T GetItem<T>(string name) where T : ItemInfo;
}
