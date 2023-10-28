using System;

public interface IItemState
{
    public event Action StateChanged; 
    int Amount { get; set; }
    bool IsEquipped { get; }

    void UnEquipped();
    void Equipped();
}
