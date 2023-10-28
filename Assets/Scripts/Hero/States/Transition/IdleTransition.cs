using DefaultNamespace.UI.Input;
using Hero.States.State;
using Hero.States.StateMachine;
using Hero.States.Transition;
using Infrastructure.Services;
using UnityEngine;

namespace Hero.Transition
{
    public class IdleTransition : ITransition
    {
        public PlayerStateMachine PlayerStateMachine => _playerStateMachine;

        private readonly IInputService _inputService;
        private readonly PlayerStateMachine _playerStateMachine;

        public IdleTransition(IInputService inputService, PlayerStateMachine playerStateMachine)
        {
            _inputService = inputService;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter() => 
            _inputService.AxisChange += OnChangeAxis;

        public void Update()
        {
        }

        public void Exit() => 
            _inputService.AxisChange -= OnChangeAxis;

        private void Transit() => 
            PlayerStateMachine.Enter<IdleState>();

        private void OnChangeAxis(Vector2 axis)
        {
            if(axis == Vector2.zero)
                Transit();
        }
    }
}
