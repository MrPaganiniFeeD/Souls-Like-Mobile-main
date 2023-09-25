using Inventory.Item.EquippedItem.Weapon;

public interface IWeaponSlot : IEquippedSlot
{
    bool TryEquip(WeaponItem item, LocationWeaponInHandType whichHandInHandType);
    bool TryUnequip(LocationWeaponInHandType locationWeaponInHandType);
}
