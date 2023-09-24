namespace Fabrics
{
    public interface IFabricAttack
    {
        IAttack GetCreationAttack(AttackType type);
    }
}