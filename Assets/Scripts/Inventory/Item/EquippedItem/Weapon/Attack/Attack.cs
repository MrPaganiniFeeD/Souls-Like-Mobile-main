using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory.Item.EquippedItem.Weapon.Attack
{
    [Serializable]
    public class AttackData : IAttackData
    {
        [SerializeField] private string _name;
        [SerializeField] private AttackType _attackType;
        [SerializeField] private int _damage;
        [SerializeField] private float _duration;
        [FormerlySerializedAs("_manaCost")] [SerializeField] private int staminaCost;
        [SerializeField] private float _forceMove;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private AudioClip _attackClip;
        
        public string Name => _name;

        public AttackType AttackType => _attackType;

        public AnimationCurve Curve => _curve;
        public int Damage => _damage;
        public AudioClip AttackAudioClip => _attackClip;
        public float Duration => _duration;
        public int StaminaCost => staminaCost;
        public float ForceMove => _forceMove;
    }
}