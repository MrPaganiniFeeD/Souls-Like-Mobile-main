using System;
using System.Collections.Generic;
using Bot.Transition;
using Hero;
using NTC.Global.System;
using Hero.States.State;
using Hero.States.Transition;
using Hero.Stats;
using UnityEngine;
using UnityEngine.AI;

internal class EnemyStateFabric : MonoBehaviour
{
    [SerializeField] private FollowStateDate _followStateDate;
    [SerializeField] private AttackEnemyStateData _attackEnemyDataState;


    private EnemyStateMachine _enemyStateMachine;
    private Player _player;
    private EnemyStateAnimator _enemyStateAnimator;
    private NavMeshAgent _navMeshAgent;
    private Stat _health;
    private EnemyData _enemyData;
    private AudioSource _audioSource;
    private RagdollOperations _ragDollOperations;

    public void Awake()
    {
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _enemyStateAnimator = GetComponent<EnemyStateAnimator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _ragDollOperations = GetComponent<RagdollOperations>();
    }

    
    public void Construct(Player player) =>
        _player = player;

    public void Construct(Stat health, EnemyData enemyData)
    {
        _health = health;    
        _enemyData = enemyData;
    }

    public Dictionary<Type, IState> CreateStates(IEnumerable<EnemyStateInfo> stateInfos)
    {
        Dictionary<Type, IState> allStates = new Dictionary<Type, IState>();
        foreach (var enemyStateInfo in stateInfos)
        {
            switch (enemyStateInfo.EnemyState)
            {
                case TypeEnemyState.Idle:
                    allStates.Add(typeof(IdleEnemyState), new IdleEnemyState(
                        CreateTransitions(enemyStateInfo.TypeEnemyTransitions),
                        _enemyStateAnimator));
                    break;
                case TypeEnemyState.Follow:
                    allStates.Add(typeof(FollowEnemyState), new FollowEnemyState(
                        CreateTransitions(enemyStateInfo.TypeEnemyTransitions),
                        _enemyStateAnimator,
                        _navMeshAgent,
                        _player.transform,
                        _followStateDate,
                        GetComponent<AudioSource>()));
                    break;
                case TypeEnemyState.Attack:
                    allStates.Add(typeof(AttackEnemyState), new AttackEnemyState(
                        CreateTransitions(enemyStateInfo.TypeEnemyTransitions),
                        _enemyStateAnimator,
                        _attackEnemyDataState,
                        _player.transform,
                        transform,
                        _enemyStateMachine,
                        _audioSource,
                        new BaseMoveModule(transform,
                            Camera.main,
                            GetComponent<Rigidbody>(),
                            this)));
                    break;
                case TypeEnemyState.TakeDamage:
                    allStates.Add(typeof(TakeDamageEnemyState), 
                        new TakeDamageEnemyState(
                            CreateTransitions(enemyStateInfo.TypeEnemyTransitions),
                            _enemyStateAnimator,
                            GetComponent<Rigidbody>(),
                            _enemyStateMachine,
                            transform,
                            _health,
                            GetComponent<AudioSource>(),
                            _enemyData.TakeDamageAudioClip));
                    break;
                case TypeEnemyState.Death:
                    allStates.Add(typeof(DeathEnemyState),
                        new DeathEnemyState(
                            CreateTransitions(enemyStateInfo.TypeEnemyTransitions),
                            _enemyStateAnimator,
                            _health,
                            GetComponent<AudioSource>(),
                            _ragDollOperations,
                            GetComponentInChildren<Lockable>()));
                    break;
            }
        }
    
        return allStates;
    }

    private List<ITransition> CreateTransitions(IEnumerable<TypeEnemyTransition> enemyTransitions)
    {
        List<ITransition> transitions = new List<ITransition>();
        foreach (var enemyTransition in enemyTransitions)
        {
            switch (enemyTransition)
            {
                case TypeEnemyTransition.Idle:
                    transitions.Add(new IdleEnemyTransition());
                    break;
                case TypeEnemyTransition.Follow:
                    transitions.Add(new FollowTransition(_enemyStateMachine,
                        _player.transform, 
                        transform,
                        _followStateDate.FollowTransitionDate));
                    break;
                case TypeEnemyTransition.Attack:
                    transitions.Add(new AttackEnemyTransition(_enemyStateMachine,
                        _player.transform,
                        transform,
                        _attackEnemyDataState.AttackEnemyDataTransition));
                    break;
                case TypeEnemyTransition.TakeDamage:
                    transitions.Add(new TakeDamageEnemyTransition(_enemyStateMachine,
                        GetComponent<IDamageDetection>()));
                    break;
            }
        }

        return transitions;
    }

}