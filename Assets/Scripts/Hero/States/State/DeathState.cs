using Hero.States.StateMachine;

namespace Hero.States.State
{
    public class DeathState : PlayerState
    {
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly PlayerStateAnimator _stateAnimator;

        public DeathState(PlayerStateMachine playerStateMachine,
            PlayerStateAnimator stateAnimator)
        {
            _playerStateMachine = playerStateMachine;
            _stateAnimator = stateAnimator;
        }

        public override void Enter()
        {
            _stateAnimator.StartDeathAnimation();
        }
    }
}