using PlayerLogic.State.StateMachine;
using PlayerLogic.States.StateMachine;
using PlayerLogic.States.Transition;
using UnityEngine;

namespace Bot.Transition
{
    public class DeathEnemyTransition : ITransition
    {
        /*public PlayerStateMachine PlayerStateMachine { get; }

        private IDamageDetection _damageDetection;

        public DeathEnemyTransition(PlayerStateMachine playerStateMachine, IDamageDetection damageDetection)
        {
            PlayerStateMachine = playerStateMachine;
            _damageDetection = damageDetection;
        }
        public void Enter()
        {
            _damageDetection.TakingBodyDamage += OnTakingBodyDamage;
            _damageDetection.TakingDamage += OnTakingDamage;
        }

        public void Exit()
        {
            _damageDetection.TakingBodyDamage -= OnTakingBodyDamage;
            _damageDetection.TakingDamage -= OnTakingDamage;
        }

        public void Update()
        {
            
        }

        private void OnTakingBodyDamage(Rigidbody attachedBody, int damage)
        {
            CheckDamage(damage);
        }

        private void OnTakingDamage(int damage)
        {
            CheckDamage(damage);
        }

        private void CheckDamage(int damage)
        {
            if(damage > 1)
                Transit();
        }
        public void Transit()
        {
            Debug.Log("Death Transition");
            PlayerStateMachine.SwitchStateThroughTheBlock(TypeEnemyState.Death);
        }*/
        public PlayerStateMachine PlayerStateMachine { get; }
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void OnLeftHandAttackButtonUp()
        {
            throw new System.NotImplementedException();
        }
    }
}