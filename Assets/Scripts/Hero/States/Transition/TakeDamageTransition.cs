using Hero.States.StateMachine;
using w.States.State;

namespace Hero.States.Transition
{
    public class TakeDamageTransition : ITransition
    {
        private readonly PlayerStateMachine _stateMachine;
        private readonly IDamageDetection _damageDetection;

        public TakeDamageTransition(PlayerStateMachine stateMachine,
            IDamageDetection damageDetection)
        {
            _stateMachine = stateMachine;
            _damageDetection = damageDetection;
        }   
        
        public void Enter()
        {
            _damageDetection.TakingDamage += Transit;
        }

        public void Update() { }

        public void Exit() => 
            _damageDetection.TakingDamage -= Transit;

        private void Transit(int damage) => 
            _stateMachine.Enter<TakeDamageState, ITakeDamageStatePayloaded>(new TakeDamageStatePayloaded(damage));
    }
}