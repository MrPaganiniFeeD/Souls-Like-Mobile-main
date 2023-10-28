using DefaultNamespace.Enemy.Factory;
using Hero;
using Infrastructure.AssetsManagement;
using Infrastructure.Factory;
using Hero.States;
using UI.Bars;
using UnityEngine;

public class EnemyFabric
{
    private readonly Player _player;
    private readonly IAssets _assets;
    private readonly PlayerCamera _playerCamera;
    private readonly IGameFactory _gameFactory;

    public EnemyFabric(Player player, IAssets assets, PlayerCamera playerCamera)
    {
        _player = player;
        _assets = assets;
        _playerCamera = playerCamera;
    }

    public GameObject CreateMonster(MonsterTypeId monsterTypeId, Vector3 position)
    {
        GameObject monster = null;
        switch (monsterTypeId)
        {
            case MonsterTypeId.GoblinWithAxe:
                monster = _assets.InstantiateNonZenject(AssetsPath.GoblinWithAxe, position);
                monster.GetComponent<EnemyStateFabric>().Construct(_player);
                monster.GetComponentInChildren<EnemyHealthBar>()
                    .Construct(_playerCamera
                        .GetComponentInChildren<Camera>());
                break;
            
        }
        return monster;
    }
    
}
