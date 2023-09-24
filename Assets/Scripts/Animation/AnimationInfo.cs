using System;
using UnityEngine;

namespace Animation
{
    [Serializable]
    public class AnimationInfo : IAnimationInfo
    {
        [SerializeField] private bool _shield;
        [SerializeField] private bool _aiming;
        [SerializeField] private int _weaponNumberAnimation;
        [SerializeField] private int _leftRightAnimation;
        [SerializeField] private int _leftWeaponAnimation;
        [SerializeField] private int _rightWeaponAnimation;
        [SerializeField] private int _attackSideAnimation;
        
        public bool Shield => _shield;
        public bool Aiming => _aiming;
        public int WeaponNumber => _weaponNumberAnimation;
        public int LeftRight => _leftRightAnimation;
        public int RightWeapon => _rightWeaponAnimation;
        public int LeftWeapon => _leftWeaponAnimation;
        public int AttackSide => _attackSideAnimation;
    }
}