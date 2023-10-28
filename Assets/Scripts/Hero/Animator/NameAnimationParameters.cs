using UnityEngine;

public static class NameAnimationParameters
{
    public static readonly int Moving = Animator.StringToHash("Moving");
    public static readonly int VelocityZ = Animator.StringToHash("Velocity Z");
    public static readonly int VelocityX = Animator.StringToHash("Velocity X");
    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int BackRolling = Animator.StringToHash("BackRollTrigger");
    public static readonly int Rolling = Animator.StringToHash("RollTrigger");
    public static readonly int TakeDamage = Animator.StringToHash("GetHitTrigger");
    public static readonly int Dead = Animator.StringToHash("Death1Trigger");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int NumberComboAttack = Animator.StringToHash("NumberComboAttack");
    public static readonly int Weapon = Animator.StringToHash("Weapon");
    public static readonly int LeftWeapon = Animator.StringToHash("LeftWeapon");
    public static readonly int RightWeapon = Animator.StringToHash("RightWeapon");
    public static readonly int AttackSide = Animator.StringToHash("AttackSide");
}