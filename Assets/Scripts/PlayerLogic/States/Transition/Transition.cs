using System;
using System.Collections;
using System.Collections.Generic;
using PlayerLogic.State.StateMachine;
using PlayerLogic.States.StateMachine;
using UnityEngine;
using Zenject;

public abstract class Transition : MonoBehaviour
{
    protected bool IsReady;
    
    protected PlayerStateMachine PlayerStateMachine { get; private set; }

    [Inject]
    public void Constructor()
    {
                
    }

    public abstract void Transit();

}
