using Hero.States.StateMachine;
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
