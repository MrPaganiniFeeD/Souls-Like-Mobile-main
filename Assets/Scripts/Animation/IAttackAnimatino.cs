namespace Animation
{
    public interface IAttackAnimation
    {
        void ActivateLeftAttack();
        void ActivateRightAttack();
        void ActivateAttack();

        void SetIsLeftHandComboState(bool isComboState);
        void SetIsRightHandComboState(bool isComboState);
        void SetIsComboState(bool isComboState);
        void ResetAnimation();
    }
}