using System.Collections;
using System.Collections.Generic;
using PlayerLogic.State.StateMachine;
using PlayerLogic.States.StateMachine;
using PlayerLogic.States.Transition;
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
