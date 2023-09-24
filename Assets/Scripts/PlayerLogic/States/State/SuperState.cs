namespace PlayerLogic.State
{
    public abstract class SuperState
    {
        /*
        public SuperState(List<BaseState> states, List<ITransition> transitions, PlayerStateMachine basePlayerStateMachine)
        {
            States = states;
            Transitions = transitions;
            BasePlayerStateMachine = basePlayerStateMachine;
        }
        public bool IsLooping { get; protected  set;}
        public bool IsBlocked { get; protected  set;}
        public List<BaseState> States { get; protected set; }
        public List<ITransition> Transitions { get; protected set; }
        protected PlayerStateMachine BasePlayerStateMachine;

        public virtual void Enter()
        {
            BasePlayerStateMachine.InitStates();
            foreach (var transition in Transitions)
            {
                transition.Enter();
            }
        }
        public virtual void Update()
        {
            BasePlayerStateMachine.Update();
        }
        public virtual void Exit()
        {
            BasePlayerStateMachine.ClearState();
            foreach (var transition in Transitions)
            {
                transition.Exit();
            }
        }
    */
    }
}