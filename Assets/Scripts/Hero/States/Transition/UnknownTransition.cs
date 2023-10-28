using Hero.States.StateMachine;
using Hero.States.Transition;
using UnityEngine;

public class UnknownTransition : ITransition
{
    public bool IsReady { get; }
    public PlayerStateMachine PlayerStateMachine { get; }
    public void Enter()
    {
        Debug.Log("Unknown Transition");
    }

    public void Exit()
    {
    }

    public void Update()
    {
        
    }

    public void Transition()
    {
        Debug.Log("Unknown Transition transit");
    }
}
