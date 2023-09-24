using Inventory.Item.EquippedItem.Weapon;

namespace DefaultNamespace.ModelData
{
    public interface IModelInfo
    {
        bool IsNacktModelUnload { get; }
        string[] Names { get; }
        LocationWeaponInHandType ModelInHand{ get; }
    }
}