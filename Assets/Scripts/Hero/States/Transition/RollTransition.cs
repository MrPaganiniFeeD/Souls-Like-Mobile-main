using System;
using Hero.States.State;
using Hero.States.StateMachine;
using Hero.Stats;
using Infrastructure.Services;
using UnityEngine;

namespace Hero.States.Transition
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
            if (_playerStats.Stamina.TryUse(_rollStateData.CostTransition))
                _stateMachine.Enter<RollState>();
        }
    }
}