using DefaultNamespace.UI.Input;
using Infrastructure.Services;
using PlayerLogic.States.State;
using PlayerLogic.States.StateMachine;
using PlayerLogic.States.Transition;
using UnityEngine;

namespace PlayerLogic.Transition
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
            _inputService.ChangeAxis += OnChangeAxis;

        public void Update()
        {
        }

        public void Exit() => 
            _inputService.ChangeAxis -= OnChangeAxis;

        private void Transit() => 
            PlayerStateMachine.Enter<IdleState>();

        private void OnChangeAxis(Vector2 axis)
        {
            if(axis == Vector2.zero)
                Transit();
        }
    }
}
