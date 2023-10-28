using System.Collections.Generic;
using Hero.States.Transition;

namespace Hero.States.State
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
