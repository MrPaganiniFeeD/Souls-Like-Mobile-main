using System;
using System.Collections.Generic;
using PlayerLogic.States.State;
using PlayerLogic.Stats;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private bool _isLocked;
    [SerializeField] private List<EnemyStateInfo> _stateInfos;

    public Action<IState> UpdateCurrentState;
    public IState CurrentState => _currentState;

    private Dictionary<Type, IState> _allState;
    private IState _currentState;
    
    private Transform _transform;
    private EnemyStateFabric _enemyStateFabric;
    private EnemyData _enemyDate;
    private Stat _health;

    private void Awake()
    {
        _enemyStateFabric = GetComponent<EnemyStateFabric>();
        _transform = transform;
    }

    private void Start()
    {
        _allState = _enemyStateFabric.CreateStates(_stateInfos);
        Enter<IdleEnemyState>();
    }

    public void Construct(Stat health, EnemyData enemyData)
    {
        _health = health;
        _enemyDate = enemyData;
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
        where TState : class, IState, IEnemyState<TPayloaded> where TPayloaded : IEnemyStatePayloaded
    {
        if(_isLocked)
            return;
        IEnemyState<TPayloaded> state = ChangeState<TState>();
        state.Enter(payloaded);
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
}