using System.Collections.Generic;
using PlayerLogic.States.Transition;

namespace PlayerLogic.States.State
{
    public class UnknownState : PlayerState 
    {
        public UnknownState(List<ITransition> transitions) : base(transitions)
        {
        }

        public UnknownState()
        {
            
        }

    }
}
