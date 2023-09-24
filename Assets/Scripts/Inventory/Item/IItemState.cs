using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemState 
{
    int Amount { get; set; }
    bool IsEquipped { get; }

    void UnEquipped();
}
