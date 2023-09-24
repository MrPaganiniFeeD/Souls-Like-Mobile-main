public class ItemState : IItemState
{
    private int _itemAmount;
    private bool _isItemEquipped;

    public int Amount { get => _itemAmount; set => _itemAmount = value; }
    public bool IsEquipped { get => _isItemEquipped; set => _isItemEquipped = value; }
    
    public ItemState()
    {
        _itemAmount = 1;
        _isItemEquipped = false;
    }

    public void UnEquipped()
    {
        IsEquipped = false;
    }
}
