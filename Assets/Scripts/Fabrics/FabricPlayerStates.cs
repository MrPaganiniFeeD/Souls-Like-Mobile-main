using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services;
using Infrastructure.Services.Inventory;
using PlayerLogic.Animation;
using PlayerLogic.States;
using PlayerLogic.States.State;
using PlayerLogic.States.StateMachine;
using PlayerLogic.States.Transition;
using PlayerLogic.Stats;
using UnityEngine;

namespace Fabrics
{
    public class FabricPlayerStates : IFabricState
    {
        
        private IFabricTransition _fabricTransitions;
        private IInputService _inputService;
        private readonly IInventoryService _inventoryService;

        private PlayerStats _playerStats;
        private IWeaponSlot _leftWeaponSlot;
        private IWeaponSlot _rightWeaponSlot;
        private Player _player;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly PlayerStateAnimator _animator;
        private readonly PlayerCamera _playerCamera;
        private readonly RollStateData _rollStateData;
        private IEnumerable<PlayerStateInfo> _stateInfos;
        private BaseMoveModule _baseMove;
        private AudioSource _audioSource;
        private CharacterController _characterController;

        public FabricPlayerStates(IInputService inputService,
            IInventoryService inventoryService,
            PlayerStats playerStats,
            IFabricTransition fabricTransition,
            Player player,
            PlayerStateMachine playerStateMachine,
            PlayerStateAnimator playerStateAnimator,
            PlayerCamera playerCamera,
            RollStateData rollStateData)
        {
            _inputService = inputService;
            _inventoryService = inventoryService;
            _playerStats = playerStats;
            _fabricTransitions = fabricTransition;
            _player = player;
            _playerStateMachine = playerStateMachine;
            _animator = playerStateAnimator;
            _playerCamera = playerCamera;
            _rollStateData = rollStateData;

            _characterController = _player.GetComponent<CharacterController>();
            _baseMove = new BaseMoveModule(_player.transform,
                Camera.main,
                _characterController,
                monoBehaviour: _player);
            
        }
        public Dictionary<Type, IState> CreateStates(IEnumerable<PlayerStateInfo> stateInfos)
        {
            _stateInfos = stateInfos;
            _audioSource = _player.GetComponent<AudioSource>();
            var allState = new Dictionary<Type, IState>
            {
                [typeof(IdleState)] = new IdleState(GetTransition(TypePlayerState.Idle),
                    _animator),
                [typeof(PlayerMoveState)] = new PlayerMoveState(GetTransition(TypePlayerState.Move),
                    _inputService, 
                     Camera.main,
                    _player.transform,
                    _characterController,
                    _animator,
                    _inventoryService,
                    _audioSource),
                [typeof(AttackState)] = new AttackState(GetTransition(TypePlayerState.Attack),
                    _playerStateMachine,
                    _inventoryService.InventoryEquipped,
                    _inputService,
                    _playerStats,
                    _animator,
                    _baseMove,
                    _audioSource,
                    _playerCamera,
                    _player.transform),
                [typeof(RollState)] = new RollState(GetTransition(TypePlayerState.Roll),
                    _playerStateMachine,
                    _animator,
                    _inputService,
                    _baseMove,
                    _rollStateData,
                    _characterController)
            };
            return allState;
        }

        private IFabricAttack GetCreationFabricAttack() =>
            new FabricAttack(_animator,
                Camera.main,
                _player,
                _inputService);
        
        private List<ITransition> GetTransition(TypePlayerState stateType)
        {
            foreach (var stateInfo in _stateInfos.Where(stateInfo => stateInfo.State == stateType))
                return _fabricTransitions.CreatTransitions(stateInfo.Transitions);
            return null;
        }
    }
}
