using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Create New Items", order = 51)]
public class ItemInfo : ScriptableObject, IItemInfo
{
    [SerializeField] private string _name;
    [SerializeField] private string _discription;
    
    [Min(1)]
    [SerializeField] private int _maxItemsInInventorySlot;

    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _pefab;

    public int ID => _id;
    public string Name => _name;
    public string Discription => _discription;
    public int MaxItemsInInventorySlot => _maxItemsInInventorySlot;
    public Sprite Icon => _icon;
    public GameObject Prefab => _pefab;
    
    private int _id;

    public void SetID(int id) => 
        _id = id;
    
    public Item GetCreationItem() =>
        new DefaultItem(this);
}