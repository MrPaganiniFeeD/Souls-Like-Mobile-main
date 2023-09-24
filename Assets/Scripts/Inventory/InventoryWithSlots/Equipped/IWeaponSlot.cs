using Inventory.Item.EquippedItem.Weapon;

public interface IWeaponSlot<T> : IEquippedSlot<EquippedItem> where T : WeaponItem
{
    bool TryEquip(WeaponItem item, LocationWeaponInHandType whichHandInHandType);
    bool TryUnequip(LocationWeaponInHandType locationWeaponInHandType);
}
