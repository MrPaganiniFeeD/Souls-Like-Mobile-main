using Infrastructure.Services;
using PlayerLogic.States.State;
using PlayerLogic.States.StateMachine;

namespace PlayerLogic.States.Transition
{
    public class AttackTransition : ITransition
    {
        public PlayerStateMachine PlayerStateMachine { get; }
        private readonly IInputService _inputService;

        public AttackTransition(PlayerStateMachine playerStateMachine, IInputService inputService)
        {
            PlayerStateMachine = playerStateMachine;
            _inputService = inputService;
        }

        public void Enter()
        {
            _inputService.LeftHandAttackButtonUp += OnLeftHandAttackButtonUp;
            _inputService.RightHandAttackButtonUp += OnRightHandAttackButtonUp;
            _inputService.MainAttackButtonUp += OnMainAttackUp;
        }

        public void Update()
        {
        
        }

        public void Exit()
        {
            _inputService.LeftHandAttackButtonUp -= OnLeftHandAttackButtonUp;
            _inputService.RightHandAttackButtonUp -= OnRightHandAttackButtonUp;
            _inputService.MainAttackButtonUp -= OnMainAttackUp;
        }

        public void OnLeftHandAttackButtonUp() => 
            Transit(ButtonType.LeftButton);

        private void OnRightHandAttackButtonUp() => 
            Transit(ButtonType.RightButton);

        private void OnMainAttackUp() => 
            Transit(ButtonType.MainButton);

        private void Transit(ButtonType button) => 
            PlayerStateMachine.Enter<AttackState, IAttackStatePayloaded>(new AttackStatePayloaded(button));
    }
}
