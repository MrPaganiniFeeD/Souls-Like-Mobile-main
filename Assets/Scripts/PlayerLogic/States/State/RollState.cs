using System;
using System.Collections.Generic;
using Infrastructure.Services;
using PlayerLogic.Animation;
using PlayerLogic.States.StateMachine;
using PlayerLogic.States.Transition;
using PlayerLogic.Stats;
using UnityEngine;

namespace PlayerLogic.States.State
{
    public class RollState : PlayerState
    {
        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerStateAnimator _stateAnimator;
        private readonly PlayerStats _playerStats;
        private readonly IInputService _inputService;
        private readonly IMoveModule _moveModule;
        private readonly RollStateData _rollStateData;
        private readonly CharacterController _characterController;
        private readonly Transform _transform;
        private Vector3 _defaultCenterCollider;
        private float _defaultHigh;

        public RollState(List<ITransition> transitions,
            PlayerStateMachine stateMachine,
            PlayerStateAnimator stateAnimator,
            PlayerStats playerStats,
            IInputService inputService,
            IMoveModule moveModule,
            RollStateData rollStateData,
            CharacterController characterController) : base(transitions)
        {
            _stateMachine = stateMachine;
            _stateAnimator = stateAnimator;
            _playerStats = playerStats;
            _inputService = inputService;
            _moveModule = moveModule;
            _rollStateData = rollStateData;
            _characterController = characterController;
            _transform = characterController.transform;

            _defaultCenterCollider = _characterController.center;
            _defaultHigh = _characterController.height;
        }
        

        public override void Enter()
        {
            base.Enter();
            _stateAnimator.ExitState += TransitionToIdle;
            _playerStats.Stamina.Value -= _rollStateData.CostTransition;
            
            Vector3 direction = _inputService.Axis;
            if (direction == Vector3.zero)
            {
                direction = - 1 * _transform.forward;
                _stateAnimator.StartBackRollAnimation();
            }
            else
            {
                direction = _transform.forward;
                _stateAnimator.StartForwardRollAnimation();
            }

            DownSizeCollider();
            Move(direction);
        }

        public override void Exit()
        {
            base.Exit();
            _stateAnimator.ExitState -= TransitionToIdle;
            SetDefaultSizeCollider();
        }

        private void DownSizeCollider() => 
            SetSizeCollider(_rollStateData.Center, _rollStateData.High);

        private void SetDefaultSizeCollider() => 
            SetSizeCollider(_defaultCenterCollider, _defaultHigh);

        private void SetSizeCollider(Vector3 center, float high)
        {
            _characterController.center = center;
            _characterController.height = high;
        }


        private void Move(Vector3 direction)
        {
            _moveModule.MoveAlongACurveUsingCharacterController(direction,
                _rollStateData.MoveCurve[_rollStateData.MoveCurve.length - 1].time,
                2f,
                _rollStateData.MoveCurve);
        }

        private void TransitionToIdle() => 
            _stateMachine.Enter<IdleState>();
    }
    
    
    [Serializable]
    public class RollStateData
    {
        public int CostTransition;
        public AnimationCurve MoveCurve;

        [Header("Collider Data")] 
        public Vector3 Center = new Vector3(0, 0.5f, 0);
        public float High = 1f;

    }
}