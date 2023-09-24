using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIInventory
{
    IInventory Inventory { get;}
    UIRenderInventory RenderInventory { get;}
}
