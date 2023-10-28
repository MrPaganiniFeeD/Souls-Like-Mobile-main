using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIInventory
{
    InventoryWithSlots Inventory { get;}
    UIRenderInventory RenderInventory { get;}
}
