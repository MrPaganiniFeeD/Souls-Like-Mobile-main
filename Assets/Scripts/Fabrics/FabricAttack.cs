using Infrastructure.Services;
using PlayerLogic.Animation;
using PlayerLogic.States;
using UnityEngine;

namespace Fabrics
{
    public class FabricAttack : IFabricAttack
    {
        private readonly PlayerStateAnimator _stateAnimator;
        private readonly UnityEngine.Camera _playerCamera;
        private readonly Player _player;
        private readonly IInputService _inputService;

        public FabricAttack(PlayerStateAnimator stateAnimator,
            UnityEngine.Camera playerCamera,
            Player player,
            IInputService inputService)
        {
            _stateAnimator = stateAnimator;
            _playerCamera = playerCamera;
            _player = player;
            _inputService = inputService;
        }
        
        public IAttack GetCreationAttack(AttackType type)
        {
            return type switch
            {
            
                AttackType.Bow => null,
                _ => null
            };
        }
    }
}