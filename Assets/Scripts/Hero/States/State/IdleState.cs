using System.Collections.Generic;
using Hero.States.StateMachine;
using Hero.States.Transition;

namespace Hero.States.State
{
    
    public class IdleState : PlayerState, IIdleState
    {
        private readonly PlayerStateAnimator _animator;

        public IdleState(List<ITransition> transitions,
            PlayerStateAnimator animator) : base(transitions)
        {
            _animator = animator;
        }

        public override void Enter()
        {
            base.Enter();
            _animator.StartIdleAnimation();
        }
        public override void Exit()
        {
            base.Exit();
            _animator.StopIdleAnimation();
        }
    }
}
