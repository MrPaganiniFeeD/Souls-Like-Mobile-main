using Data;
using DefaultNamespace.Enemy.Factory;
using DefaultNamespace.Logic;
using Hero;
using Infrastructure.AssetsManagement;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Hero.States;
using UnityEngine;
using Zenject;


public class EnemySpawner : MonoBehaviour, ISavedProgress
{
    [SerializeField] private MonsterTypeId MonsterTypeId;

    [SerializeField] private bool _slain;
    private string _id;
    private IGameFactory _gameFactory;
    private EnemyFabric _enemyFabric;

    public void Construct(Player player, IAssets assets, PlayerCamera playerCamera)
    {
        _enemyFabric = new EnemyFabric(player, assets, playerCamera);
    }
    private void Awake() => 
        _id = GetComponent<UniqueId>().Id;

    public void LoadProgress(PlayerProgress playerProgress)
    {
        if (playerProgress.KillData.ClearedSpawners.Contains(_id))
            _slain = true;
        else
            Spawn();
    }

    private void Spawn() => 
        _enemyFabric.CreateMonster(MonsterTypeId, transform.position);

    public void UpdateProgress(PlayerProgress playerProgress)
    {
        if (_slain)
            playerProgress.KillData.ClearedSpawners.Add(_id);
    }
}
