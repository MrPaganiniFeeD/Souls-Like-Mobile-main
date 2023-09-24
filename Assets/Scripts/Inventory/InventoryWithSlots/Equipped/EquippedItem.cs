public class EquippedItem : Item
{
    public new IEquippedItemInfo Info  { get; }

    public EquippedItem(IEquippedItemInfo info, IItemState itemState = null) : base(info, itemState)
    {
        Info = info;
    }
    
}
