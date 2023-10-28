using Hero.States.StateMachine;

namespace Hero.States.Transition
{
    public interface ITransition
    {
        void Enter();
        void Update();
        void Exit();
 
    }
}
