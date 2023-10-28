using DefaultNamespace.ModelData;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Equipped/Create New Equipped Items", order = 51)]


public class EquippedInfo : ItemInfo, IEquippedItemInfo
{
    [SerializeField] private EquippedItemType _type;
    [SerializeField] private ItemBuffStats _itemBuffStats;

    [Header("Model Info")] 
    [SerializeField] private ModelInfo _modelInfo;
    

    public IModelInfo ModelInfo => _modelInfo;
    public EquippedItemType Type => _type;
    public ItemBuffStats ItemBuffStats => _itemBuffStats;
    
    public new EquippedItem GetCreationItem() =>
        new EquippedItem(this, new ItemState());
}
