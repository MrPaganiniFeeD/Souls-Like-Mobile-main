public abstract class Item
{
    private readonly IItemInfo _info;
    public IItemInfo Info { get; }
    public IItemState State { get; }

    public Item(IItemInfo info, IItemState itemState = null)
    {
        _info = info;
        State = itemState ?? new ItemState();
    }

    public Item Clone() => 
        new DefaultItem(Info, State);
}