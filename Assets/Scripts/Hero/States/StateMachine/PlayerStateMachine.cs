using System;
using System.Collections.Generic;
using Data;
using Data.Extensions;
using Hero.States.State;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Hero.States.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private bool _isLocked;

        [SerializeField] private List<PlayerStateInfo> _stateInfos;

        public Action<IState> UpdateCurrentState;
        public IState CurrentState => _currentState;

        private Dictionary<Type, IState> _allState;
        private IState _currentState;
        
        private Transform _transform;
        private PlayerFabrics _playerFabrics;
        
        private void Awake()
        {
            _playerFabrics = GetComponent<PlayerFabrics>();
            _transform = transform;
            Debug.Log(_stateInfos.Count);
        }
        
        private void Start()
        {
            _allState = _playerFabrics.FabricState.CreateStates(_stateInfos);
            Enter<IdleState>();
        }

        private void Update() => 
            _currentState.Update();

        private void FixedUpdate() =>
            _currentState.FixedUpdate();

        public void Enter<TState>() where TState : class, IState
        {
            if(_isLocked)
                return;

            IState state = ChangeState<TState>();
            UpdateCurrentState?.Invoke(state);
            state.Enter();
        }
        
        public void Enter<TState, TPayloaded>(TPayloaded payloaded)
            where TState : class, IState, IPlayerState<TPayloaded> where TPayloaded : IPlayerStatePayloaded
        {
            if(_isLocked)
                return;

            IPlayerState<TPayloaded> state = ChangeState<TState>();
            state.Enter(payloaded);
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (CurrentLevel() == playerProgress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = playerProgress.WorldData.PositionOnLevel.Position;
                if(savedPosition != null)
                {
                    float offsetYSpawn = 0.5f;
                    _transform.position = savedPosition.AsUnityVector().AddY(offsetYSpawn);
                }
            }
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            playerProgress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(),
                _transform.position.AsVectorData());
        }
        
        private TState ChangeState<TState>() where TState : class, IState
        {
            TState newState = GetState<TState>();
            _currentState?.Exit();
            _currentState = newState;
            return newState;
        }
        
        private void AddState<TState>(TState state) where TState : class, IState => 
            _allState.Add(typeof(TState), state);

        private TState GetState<TState>() where TState : class, IState => 
            _allState[typeof(TState)] as TState;

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}