using System;
using Infrastructure.Services;
using PlayerLogic.States.State;
using PlayerLogic.States.StateMachine;
using PlayerLogic.Stats;
using UnityEngine;

namespace PlayerLogic.States.Transition
{
    public class RollTransition : ITransition, IDisposable
    {
        private PlayerStateMachine _stateMachine;
        private readonly PlayerStats _playerStats;
        private readonly IInputService _inputService;
        private readonly RollStateData _rollStateData;

        public RollTransition(PlayerStateMachine stateMachine,
            PlayerStats playerStats,
            IInputService inputService,
            RollStateData rollStateData)
        {
            _stateMachine = stateMachine;
            _playerStats = playerStats;
            _inputService = inputService;
            _rollStateData = rollStateData;
        }
        
        
        public void Enter() => 
            _inputService.RollButtonUp += TryTransition;

        public void Update() {}

        public void Exit() => _inputService.RollButtonUp -= TryTransition;

        public void Dispose() => 
            _inputService.RollButtonUp -= TryTransition;

        private void TryTransition()
        {
            if (_playerStats.Stamina.Value - _rollStateData.CostTransition > 0)
                _stateMachine.Enter<RollState>();
        }
    }
}