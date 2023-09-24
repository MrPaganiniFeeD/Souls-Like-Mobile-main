using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : EquippedItem
{
    public new IWeaponInfo Info { get; }
    public List<IAttack> Attack { get; }
    
    public GameObject SpawnedPrefab { get; private set; }

    public WeaponItem(IWeaponInfo info, IItemState itemState = null) : base(info, itemState)
    {
        Info = info;
        Attack = new List<IAttack>();
    }

    public void SetNewAttack(IAttack attack)
    {
        Attack.Add(attack);
    }

    public void SetSpawnedPrefab(GameObject gameObject) =>
        SpawnedPrefab = gameObject;
    
    public new WeaponItem Clone() =>
        new WeaponItem(Info, State);
}