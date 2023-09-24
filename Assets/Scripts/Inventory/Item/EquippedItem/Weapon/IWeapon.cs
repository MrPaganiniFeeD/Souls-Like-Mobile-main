public interface IWeapon
{
    IAttack Attack { get; }
    
    void Equip();
    void UnEquip();
}