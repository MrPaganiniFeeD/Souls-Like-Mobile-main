public class EquippedItem : Item
{
    private readonly IItemState _itemState;
    public new IEquippedItemInfo Info  { get; }

    public EquippedItem(IEquippedItemInfo info, IItemState itemState) : base(info, itemState)
    {
        _itemState = itemState;
        Info = info;
    }

    public void Equip()
    {
        
    }
}
