using Infrastructure.Services;
using PlayerLogic.States.State;
using PlayerLogic.States.StateMachine;
using UnityEngine;

namespace PlayerLogic.States.Transition
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
