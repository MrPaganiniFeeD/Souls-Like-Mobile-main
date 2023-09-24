using System;
using System.Collections.Generic;
using Infrastructure.Services;
using PlayerLogic.Animation;
using PlayerLogic.States.Transition;
using PlayerLogic.Stats;
using UnityEngine;

namespace PlayerLogic.States.State
{
    public class RollState : PlayerState
    {
        private readonly PlayerStateAnimator _playerStateAnimator;
        private readonly PlayerStats _playerStats;
        private readonly IInputService _inputService;
        private readonly IMoveModule _moveModule;
        private readonly RollStateData _rollStateData;

        public RollState(List<ITransition> transitions,
            PlayerStateAnimator playerStateAnimator,
            PlayerStats playerStats,
            IInputService inputService,
            IMoveModule moveModule,
            RollStateData rollStateData) : base(transitions)
        {
            _playerStateAnimator = playerStateAnimator;
            _playerStats = playerStats;
            _inputService = inputService;
            _moveModule = moveModule;
            _rollStateData = rollStateData;
        }

        public override void Enter()
        {
            base.Enter();
            _playerStats.Stamina.Value -= _rollStateData.CostTransition;
            var direction = _inputService.Axis;
            if (direction == Vector2.zero)
            {
                direction = new Vector2(0, -1);
                _playerStateAnimator.StartBackRollAnimation();
            }
            else
                _playerStateAnimator.StartForewardRollAnimation();

            _moveModule.MoveAlongACurveUsingCharacterController(direction,
                1f,
                1f,
                _rollStateData.MoveCurve);
        }
    }

    [Serializable]
    public class RollStateData
    {
        public int CostTransition;
        public AnimationCurve MoveCurve;
    }
}