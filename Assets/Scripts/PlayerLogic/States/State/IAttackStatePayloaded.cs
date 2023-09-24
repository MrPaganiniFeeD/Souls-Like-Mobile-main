using Inventory.Item.EquippedItem.Weapon.Attack;

namespace PlayerLogic.States.State
{
    public class AttackStatePayloaded : IAttackStatePayloaded
    {
        public ButtonType ButtonType { get; }
        public AttackData Attack { get; }

        public AttackStatePayloaded(ButtonType buttonType)
        {
            ButtonType = buttonType;
        }
    }

    public interface IAttackStatePayloaded : IPlayerStatePayloaded
    {
        ButtonType ButtonType { get; }

        AttackData Attack { get; }
    }

    public enum ButtonType
    {
        MainButton,
        LeftButton,
        RightButton
    }
}