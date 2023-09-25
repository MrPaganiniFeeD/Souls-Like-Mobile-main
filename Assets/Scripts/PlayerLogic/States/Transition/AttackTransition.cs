using Infrastructure.Services;
using PlayerLogic.States.State;
using PlayerLogic.States.StateMachine;
using PlayerLogic.Stats;

namespace PlayerLogic.States.Transition
{
    public class AttackTransition : ITransition
    {
        public PlayerStateMachine PlayerStateMachine { get; }
        private readonly IInputService _inputService;
        private readonly PlayerStats _playerStats;

        public AttackTransition(PlayerStateMachine playerStateMachine,
            IInputService inputService,
            PlayerStats playerStats)
        {
            PlayerStateMachine = playerStateMachine;
            _inputService = inputService;
            _playerStats = playerStats;
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
