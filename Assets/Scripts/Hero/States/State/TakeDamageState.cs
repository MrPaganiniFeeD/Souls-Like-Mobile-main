using System;
using System.Collections.Generic;
using Hero.States.State;
using Hero.States.StateMachine;
using Hero.States.Transition;
using Hero.Stats;

namespace w.States.State
{
    public class TakeDamageState : PlayerState, IDisposable, IPlayerState<ITakeDamageStatePayloaded>
    {
        private readonly PlayerStateAnimator _stateAnimator;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly PlayerStats _playerStats;

        public TakeDamageState(List<ITransition> transitions, PlayerStateMachine playerStateMachine,
            PlayerStateAnimator stateAnimator,
            PlayerStats playerStats) : base(transitions)
        {
            _stateAnimator = stateAnimator;
            _playerStateMachine = playerStateMachine;
            _playerStats = playerStats;
        }

        public void Enter(ITakeDamageStatePayloaded takeDamageEnemyStatePayloaded)
        {
            base.Enter();
            _stateAnimator.StartApplyDamageAnimation();
            _stateAnimator.ExitState += OnExitAnimationState;
            
            /*if (_playerStats.Health.Value - takeDamageEnemyStatePayloaded.Damage > 0)
                _playerStats.Health.Value -= takeDamageEnemyStatePayloaded.Damage;
            else
                _playerStateMachine.Enter<DeathState>();*/  
        }

        public override void Exit()
        {
            base.Exit();
            _stateAnimator.ExitState += OnExitAnimationState;
        }

        public void Dispose() => 
            _stateAnimator.ExitState += OnExitAnimationState;

        private void OnExitAnimationState() => 
            _playerStateMachine.Enter<IdleState>();
    }

    public interface ITakeDamageStatePayloaded : IPlayerStatePayloaded
    {
        public int Damage { get; set; }
    }

    public class TakeDamageStatePayloaded : ITakeDamageStatePayloaded
    {
        public int Damage { get; set; }

        public TakeDamageStatePayloaded(int damage)
        {
            Damage = damage;
        }
    }
}