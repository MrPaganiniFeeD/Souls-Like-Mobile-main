using Hero.States.State;
using Hero.States.StateMachine;
using Infrastructure.Services;
using Hero.Stats;

namespace Hero.States.Transition
{
    public class AttackTransition : ITransition
    {
        private PlayerStateMachine _stateMachine { get; }
        private readonly IInputService _inputService;

        public AttackTransition(PlayerStateMachine stateMachine,
            IInputService inputService)
        {
            _stateMachine = stateMachine;
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
            _stateMachine.Enter<AttackState, IAttackStatePayloaded>(new AttackStatePayloaded(button));
    }
}
