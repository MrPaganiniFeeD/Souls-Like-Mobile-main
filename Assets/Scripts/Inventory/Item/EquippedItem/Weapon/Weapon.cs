using UnityEngine;
using Zenject;


public class Weapon : MonoBehaviour, IWeapon
{
    public IAttack Attack => _attack;

    [SerializeField] private WeaponAttackType _attackType;
    
    private IAttack _attack;

    

    public virtual void Equip()
    {
        Debug.Log("Weapon Equip");
    }

    public void UnEquip()
    {
        Debug.Log("Weapon UnEquip");
    }

}

internal enum WeaponAttackType
{
    Melee,
    Bow
}