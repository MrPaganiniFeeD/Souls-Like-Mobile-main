using Hero.States.State;
using Hero.States.StateMachine;
using Infrastructure.Services;
using UnityEngine;

namespace Hero.States.Transition
{
    public class MoveTransition : ITransition
    {
        public PlayerStateMachine PlayerStateMachine { get; }
        private readonly IInputService _inputService;
    
        public MoveTransition(IInputService inputService, PlayerStateMachine playerStateMachine)
        {
            _inputService = inputService;
            PlayerStateMachine = playerStateMachine;
        }

        public void Enter() { }
        public void Update()
        {
            if (_inputService.Axis != Vector2.zero)
                Transit();
        }
        public void Exit() { }
        public void Transit() => 
            PlayerStateMachine.Enter<PlayerMoveState>();
    }
}
