
using System;
using System.Collections.Generic;
using Hero.States.State;
using Hero.States.Transition;

[Serializable]
public class EnemyState : IState
{
    private readonly List<ITransition> _transitions;
    protected readonly EnemyStateAnimator StateAnimator;

    protected EnemyState(List<ITransition> transitions, EnemyStateAnimator stateAnimator)
    {
        _transitions = transitions;
        StateAnimator = stateAnimator;
    }

  
        
    public virtual void Init()
    {
        
    }

    public virtual void Enter()
    {
        foreach (ITransition transition in _transitions) 
            transition.Enter();
    }

    public virtual void Update()
    {
        foreach (ITransition transition in _transitions) 
            transition.Update();
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Exit()
    {
        foreach (ITransition transition in _transitions) 
            transition.Exit();
    }

}