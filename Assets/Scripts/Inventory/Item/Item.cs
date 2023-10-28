public abstract class Item
{
    public IItemInfo Info { get; }
    public IItemState State { get; }

    protected Item(IItemInfo info, IItemState itemState = null)
    {
        Info = info;
        State = itemState ?? new ItemState();
    }

    public Item Clone() => 
        new DefaultItem(Info, State);
}