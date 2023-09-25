using System;
using DefaultNarmespace.Player.AnimatorReporter;
using Infrastructure.Services.Inventory;
using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;
using Zenject;

namespace PlayerLogic.Animation
{
    public class PlayerStateAnimator : MonoBehaviour, IAnimationStateReader, IDisposable
    {
        public event Action ExitState;
        public event Action EnterState;
        
        public event Action ExitLeftHandAttack;
        public event Action ExitRightHandAttack;
        public event Action ExitTwoHandAttack;
        public event Action EnableWeaponCollider;

        public event Action StepFootR;
        public event Action StepFootL;


        private Animator _animator;
        private WeaponSlot _weaponSlot;
        private bool _isLockOn = true;

        [SerializeField] private AudioClip _step;
        private AudioSource _audioSource;


        [Inject]
        public void Construct(IInventoryService inventoryService)
        {
            _weaponSlot = inventoryService.InventoryEquipped.WeaponSlot;
            _weaponSlot.WeaponEquipped += OnWeaponEquipped;
            _weaponSlot.WeaponUnequipped += OnWeaponUnequipped;
        }

        private void Start()
        {
            OnWeaponEquipped(_weaponSlot.LeftHandWeapon, LocationWeaponInHandType.LeftHand);   
            OnWeaponEquipped(_weaponSlot.RightHandWeapon, LocationWeaponInHandType.RightHand);   
            OnWeaponEquipped(_weaponSlot.TwoHandWeaponItem, LocationWeaponInHandType.TwoHand);

            _audioSource = GetComponent<AudioSource>();
        }

        private void OnWeaponUnequipped(WeaponItem weapon, LocationWeaponInHandType handType)
        {
            switch (handType)
            {
                case LocationWeaponInHandType.LeftHand:
                    _animator.SetInteger(NameAnimationParameters.LeftWeapon, 0);
                    break;
                case LocationWeaponInHandType.RightHand:
                    _animator.SetInteger(NameAnimationParameters.RightWeapon, 0);
                    break;
                case LocationWeaponInHandType.TwoHand:
                    _animator.SetInteger(NameAnimationParameters.Weapon, 0);
                    break;
            }    
        }

        private void OnWeaponEquipped(WeaponItem weapon, LocationWeaponInHandType arg2)
        {
            if (weapon != null)
                _animator.SetInteger(NameAnimationParameters.Weapon, weapon.Info.AnimationInfo.WeaponNumber);
        }


        private void Awake() => 
            _animator = GetComponent<Animator>();


        public void StartIdleAnimation() =>
            _animator.SetTrigger(NameAnimationParameters.Idle);

        public void StopIdleAnimation() =>
            _animator.ResetTrigger(NameAnimationParameters.Idle);


        public void StartDefaultMoveAnimation() => 
            _animator.SetBool(NameAnimationParameters.Moving, true);

        public void StopDefaultMoveAnimation() => 
            _animator.SetBool(NameAnimationParameters.Moving, false);

        public void FootR() =>
            _audioSource.PlayOneShot(_step);

        public void FootL() =>
            _audioSource.PlayOneShot(_step);
        public void UpdateVelocity(float x, float z)
        {
            _animator.SetBool(NameAnimationParameters.Moving, true);
            _animator.SetFloat(NameAnimationParameters.VelocityX, x);
            _animator.SetFloat(NameAnimationParameters.VelocityZ, z);
        }


        public void StartBackRollAnimation() => 
            _animator.SetTrigger(NameAnimationParameters.BackRolling);

        public void StartForwardRollAnimation() => 
            _animator.SetTrigger(NameAnimationParameters.Rolling);

        public void StopRollAnimation() => 
            _animator.ResetTrigger(NameAnimationParameters.Rolling);


        public void StopApplyDamageAnimation() => 
            _animator.ResetTrigger(NameAnimationParameters.TakeDamage);

        public void StartApplyDamageAnimation() => 
            _animator.SetTrigger(NameAnimationParameters.TakeDamage);


        public void StartDeathAnimation() => 
            _animator.SetTrigger(NameAnimationParameters.Dead);


        public void StartAttackAnimation(int numberAttack)
        {
            SetComboNumber(numberAttack);
            _animator.SetTrigger(NameAnimationParameters.Attack);
        }

        public void StopAttackAnimation() => 
            _animator.ResetTrigger(NameAnimationParameters.Attack);

        public void EnteredState(int hashAnimation) => 
            EnterState?.Invoke();

        public void ExitedState(int hashAnimation) =>
            ExitState?.Invoke();

        public void SetComboNumber(int numberAttack) =>
            _animator.SetInteger(NameAnimationParameters.NumberComboAttack, numberAttack);

        public void EnableCollider() => 
            EnableWeaponCollider?.Invoke();

        public void EndTwoWeaponAttack() => 
            ExitTwoHandAttack?.Invoke();

        public void EndLeftHandAttack() =>
            ExitLeftHandAttack?.Invoke();

        public void EndRightHandAttack() =>
            ExitRightHandAttack?.Invoke();

        private void ShowStateInfo(AnimatorStateInfo stateInfo)
        {
            switch (stateInfo.tagHash)
            {
                    
            }
        }

        public void SetDefaultCamera()
        {
            _animator.SetFloat(NameAnimationParameters.VelocityX, 0);
            _animator.SetFloat(NameAnimationParameters.VelocityZ, 0);
        }

        public void SetLockOnCamera(bool lockOn) => 
            _isLockOn = lockOn;

        public void OnDestroy()
        {
            _weaponSlot.WeaponEquipped -= OnWeaponEquipped;
            _weaponSlot.WeaponUnequipped -= OnWeaponUnequipped;
        }

        public void Dispose()
        {
            _weaponSlot.WeaponEquipped -= OnWeaponEquipped;
            _weaponSlot.WeaponUnequipped -= OnWeaponUnequipped;
        }
    }
}