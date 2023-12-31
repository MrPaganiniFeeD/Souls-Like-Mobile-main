
using UnityEngine;

public interface IAttackData
{
    string Name { get; }

    AttackType AttackType { get; }
    public AnimationCurve Curve { get;}
    int Damage { get; }
    float Duration { get; }
    int StaminaCost { get; }
    float ForceMove { get; }
    AudioClip AttackAudioClip { get; }

}