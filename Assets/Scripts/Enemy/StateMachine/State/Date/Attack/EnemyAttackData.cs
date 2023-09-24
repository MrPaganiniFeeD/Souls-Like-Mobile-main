using System;
using UnityEngine;

[Serializable]
public class EnemyAttackData
{
    public float AttackDistance;
    public int Damage;
    public AnimationCurve AnimationCurve;
    public AudioClip AttackAudioClip;
}