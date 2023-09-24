using UnityEngine;

namespace Animation
{
    public class WeaponAnimator : MonoBehaviour
    {
        private static readonly int Weapon = Animator.StringToHash("Weapon");
        private static readonly int Shield = Animator.StringToHash("Shield");
        private static readonly int RightWeapon = Animator.StringToHash("RightWeapon");
        private static readonly int LeftWeapon = Animator.StringToHash("LeftWeapon");
        private static readonly int LeftRight = Animator.StringToHash("LeftRight");
        private static readonly int Aiming = Animator.StringToHash("Aiming");
        private static readonly int AttackSide = Animator.StringToHash("AttackSide");
        private static readonly int InstantSwitchTrigger = Animator.StringToHash("InstantSwitchTrigger");

        private Animator _animator;
        private int _lastLeftWeaponNumber;
        private int _lastRightWeaponNumber;

        private int _comboNumber;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            SetUnarmedAnimation();
        }
        private void LeftSlotSetAnimation(IWeaponInfo weaponInfo) => 
            SetAnimationParameters(LeftWeapon, weaponInfo.AnimationInfo.LeftWeapon, weaponInfo);

        private void RightSlotSetAnimation(IWeaponInfo weaponInfo) => 
            SetAnimationParameters(RightWeapon, weaponInfo.AnimationInfo.RightWeapon, weaponInfo);

        private void DeleteLeftSlotAnimation(IWeaponInfo weaponInfo) => 
            ClearAnimationParameters(LeftWeapon);

        private void DeleteRightSlotAnimation(IWeaponInfo weaponInfo) => 
            ClearAnimationParameters(RightWeapon);

        private void SetAnimationParameters(int animationParameterID, int rightOrLeftValue, IWeaponInfo weaponInfo)
        {
            _animator.SetInteger(animationParameterID, rightOrLeftValue);
            SetMainAnimation(weaponInfo);
        }

        private void ClearAnimationParameters(int animationParameterID) => 
            _animator.SetInteger(animationParameterID, 0);

        private void SetMainAnimation(IWeaponInfo weaponInfo)
        {
            _animator.SetInteger(Weapon, weaponInfo.AnimationInfo.WeaponNumber);
            _animator.SetInteger(LeftRight ,weaponInfo.AnimationInfo.LeftRight);
            _animator.SetBool(Aiming, weaponInfo.AnimationInfo.Aiming);
            _animator.SetBool(Shield, weaponInfo.AnimationInfo.Shield);
            _animator.SetTrigger(InstantSwitchTrigger);
        }

        private void SetUnarmedAnimation()
        {
            _animator.SetInteger(Weapon, 0);
            _animator.SetTrigger(InstantSwitchTrigger);   
        }

        private void OnDestroy()
        {
        
        }
    }
}