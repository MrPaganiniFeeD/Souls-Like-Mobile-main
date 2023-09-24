using System;
using System.Collections.Generic;

namespace PlayerLogic.States.State
{
    public interface IStateInfo<out TState, TTransition> where TState : Enum
    {
        TState State { get; }
        List<TTransition> Transitions { get; }
    }
}
