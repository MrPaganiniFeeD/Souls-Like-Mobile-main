using System.Collections.Generic;
using Hero.States.Transition;

public class IdleEnemyState : EnemyState
{
    public IdleEnemyState(List<ITransition> transitions, EnemyStateAnimator stateAnimator) : base(transitions, stateAnimator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StateAnimator.StartIdleAnimation();
    }
    
}