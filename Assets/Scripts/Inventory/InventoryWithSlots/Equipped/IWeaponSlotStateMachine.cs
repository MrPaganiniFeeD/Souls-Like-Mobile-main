public interface IWeaponSlotStateMachine
{
    void Enter<T>(WeaponItem leftHandWeapon,
        WeaponItem rightHandWeapon,
        WeaponItem twoHand) where T : WeaponSlotState;
}