using System.Collections.Generic;
using Hero.States.Transition;

namespace Hero.States.State
{
    public abstract class PlayerState : IState
    {
        private readonly List<ITransition> _transitions;
        protected PlayerState(List<ITransition> transitions)
        {
            _transitions = transitions;
        }

        public PlayerState()
        {
            
        }
        
        public virtual void Init()
        {
            ;
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

    public interface IState
    {
        void Init();
        void Enter();
        void Update();
        void FixedUpdate();
        void Exit();
    }
}