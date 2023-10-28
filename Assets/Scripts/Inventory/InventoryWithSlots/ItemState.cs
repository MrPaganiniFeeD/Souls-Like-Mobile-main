using System;

public class ItemState : IItemState
{
    public event Action StateChanged;
    
    private int _itemAmount;
    private bool _isItemEquipped;

    public int Amount
    {
        get => _itemAmount;
        set
        {
            _itemAmount = value;
            StateChanged?.Invoke();
        }
    }

    public bool IsEquipped => _isItemEquipped;
    
    public ItemState()
    {
        _itemAmount = 1;
        _isItemEquipped = false;
    }

    public void UnEquipped()
    {
        _isItemEquipped = false;
        StateChanged?.Invoke();
    }

    public void Equipped()
    {
        _isItemEquipped = true;
        StateChanged?.Invoke();
    }
}
