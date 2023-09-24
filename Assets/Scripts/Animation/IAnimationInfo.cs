namespace Animation
{
    public interface IAnimationInfo
    {
        public bool Shield { get; }
        public bool Aiming { get; }

        public int WeaponNumber { get; }
        public int LeftRight { get; }
        public int RightWeapon { get; }
        public int LeftWeapon { get; }
        public int AttackSide { get; }
    }
}