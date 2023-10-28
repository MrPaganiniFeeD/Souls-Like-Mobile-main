using DefaultNamespace.Bot.Stats;
using Hero.Stats;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    public Stat Health => _health;

    [FormerlySerializedAs("EnemyData")]
    [SerializeField] private EnemyData _enemyData;

    private EnemyStateMachine _stateMachine;
    private Stat _health;

    private void Awake()
    {
        _health = new EnemyStat("Health", _enemyData.Health);
        _stateMachine = GetComponent<EnemyStateMachine>();
        _stateMachine.Construct(_health, _enemyData);
        GetComponent<EnemyStateFabric>().Construct(_health, _enemyData);
    }
    

}
