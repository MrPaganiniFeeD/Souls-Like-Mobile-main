using System;
using DefaultNarmespace.Player.AnimatorReporter;
using UnityEngine;

public class EnemyStateAnimator : MonoBehaviour, IAnimationStateReader
{
    public event Action AttackIsOver;
    public event Action EndGetHitAnimation;
    public event Action ExitState;
    public event Action EnterState;
    public event Action StepFootLR;
    public event Action HitWeapon;

    [SerializeField] private int _weaponNumber;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Patrol = Animator.StringToHash("Patrol");
    private static readonly int ComboAttack = Animator.StringToHash("ComboAttack");
    private static readonly int Attack = Animator.StringToHash("AttackTrigger");
    private static readonly int TakeDamage = Animator.StringToHash("GetHitTrigger");
    private static readonly int Dead = Animator.StringToHash("Death1Trigger");
    private static readonly int Idle = Animator.StringToHash("IdleTrigger");
    private static readonly int Weapon = Animator.StringToHash("Weapon");
    private static readonly int VelocityX = Animator.StringToHash("Velocity X");
    private static readonly int VelocityZ = Animator.StringToHash("Velocity Z");
    private static readonly int NumberComboAttack = Animator.StringToHash("NumberComboAttack");

    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetInteger(Weapon, _weaponNumber);
    }

    public void StartIdleAnimation() =>
        _animator.SetTrigger(Idle);
    public void ResetIdleAnimation() => 
        _animator.ResetTrigger(Idle);

    public void StartFollowAnimation() => 
        _animator.SetBool(Moving, true);

    public void UpdateVelocity(float velocityX, float velocityZ)
    {
        _animator.SetFloat(VelocityX, velocityX);
        _animator.SetFloat(VelocityZ, velocityZ);
    }

    public void UpdateSpeed(float speed) => 
        SetSpeed(speed);

    public void StopFollowAnimation()
    {
        _animator.SetBool(Moving, false);
        SetSpeed(0);
    }
    
    public void StartPatrolAnimation(float speed)
    {
        _animator.SetTrigger(Patrol);
        SetSpeed(speed);
    }

    public void StopPatrolAnimation()
    {
        _animator.ResetTrigger(Patrol);
        SetSpeed(0);
    }

    public void StartAttackAnimation(int numberAttack)
    {
        _animator.SetTrigger(Attack);
        _animator.SetInteger(NumberComboAttack, numberAttack);
    }

    public void StopAttackAnimation() => 
        _animator.ResetTrigger(Attack);

    public void StartComboAttackAnimation() => 
        _animator.SetBool(ComboAttack, true);

    public void StopComboAttackAnimation() => 
        _animator.SetBool(ComboAttack, false);

    public void StartDieAnimation() => 
        _animator.SetTrigger(Dead);

    public void StopDieAnimation() => 
        _animator.ResetTrigger(Dead);

    public void Hit() =>
        HitWeapon?.Invoke();

    public void StartApplyDamageAnimation() => 
        _animator.SetTrigger(TakeDamage);

    public void FootR() =>
        StepFootLR?.Invoke();

    public void FootL() =>
        StepFootLR?.Invoke();

    public void EndGetHit() =>
        EndGetHitAnimation?.Invoke();
    public void EnteredState(int hashAnimation) => 
        throw new NotImplementedException();

    public void ExitedState(int hashAnimation) => 
        throw new NotImplementedException();

    private void SetSpeed(float speed) =>
        _animator.SetFloat(Speed, speed);


    public void EndAttack() => 
        AttackIsOver?.Invoke();

    public void Disable()
    {
        _animator.enabled = false;
        this.enabled = false;
    }
}
