using PlayerLogic.States.StateMachine;

namespace PlayerLogic.States.Transition
{
    public interface ITransition
    {
        void Enter();
        void Update();
        void Exit();
 
    }
}
