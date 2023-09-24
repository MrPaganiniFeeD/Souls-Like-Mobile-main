using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IApplyingItemStats
{
    void Equip(ItemBuffStats itemBuffStats, object source);
    void UnEquip(object source);
}
