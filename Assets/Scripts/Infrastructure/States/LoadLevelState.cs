using DefaultNamespace.UI.Scene;
using Infrastructure.AssetsManagement;
using Infrastructure.DI;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using PlayerLogic.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.States
{
    public class LoadLevelGameState : IPayloadedGameState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string EnemySpawnerTag = "EnemySpawner";

        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IInputService _inputService;
        private readonly IAssets _assets;
        private readonly IGameFactory _gameFactory;
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private LoadingCurtain _loadingCurtain;
        private readonly DiContainer _diContainer;
        private PlayerCamera _camera;


        public LoadLevelGameState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            DiContainer diContainer,
            IGameFactory gameFactory,
            IPersistentProgressService persistentProgressService,
            IInputService inputService,
            IAssets assets)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
            _inputService = inputService;
            _assets = assets;
            _sceneLoader = sceneLoader;
            _diContainer = diContainer;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain = InstantiateLoadingCurtain();
            _loadingCurtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.ChangeProgress += OnChangeProgressLoading;
            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }
        
        public void Exit()
        {
            _loadingCurtain.Hide();
            _sceneLoader.ChangeProgress -= OnChangeProgressLoading;
        }

        private void OnChangeProgressLoading(float value) => 
            _loadingCurtain.UpdateProgress(value);

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            EnableMusic();
            _stateMachine.Enter<GameLoopState>();
        }

        private void EnableMusic()
        {
            var soundService = _gameFactory.CreateSoundService();
            soundService.EnableMusic();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressesReader)
                progressReader.LoadProgress(_persistentProgressService.PlayerProgress);
        }

        private void InitGameWorld()
        {
            Player player = CreatePlayer();
            CameraInit(player);
            InitSpawners(player, _camera);
            CreateHud();
            CreateLockOnUI(_camera);
        }

        private void CameraInit(Player player)
        {
            _camera = Camera.main.GetComponentInParent<PlayerCamera>();
            _camera.Construct(player, _inputService);
        }

        private void InitSpawners(Player player, PlayerCamera playerCamera)
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
                EnemySpawner spawner = gameObject.GetComponent<EnemySpawner>();
                spawner.Construct(player, _assets, playerCamera);
                _gameFactory.Register(spawner);
            }
            
        }
        private void CreateHud() => 
            _gameFactory.CreateHud();

        private void CreateLockOnUI(PlayerCamera camera) => 
            _gameFactory.CreateLockOnUI().GetComponent<LockOnTargetUI>().Construct(camera);

        private LoadingCurtain InstantiateLoadingCurtain()
        {
            GameObject loadingCurtainPrefab = _gameFactory.CreateScreenLoading();
            return loadingCurtainPrefab.GetComponent<LoadingCurtain>();
        }
        

        private Player CreatePlayer()
        {
            GameObject spawnObject = GameObject.FindWithTag(InitialPointTag);
            GameObject playerPrefab = _gameFactory.CreatePlayer(spawnObject);
            Player player = playerPrefab.GetComponent<Player>();
            DiContainerSceneRef.Container.Bind<Player>().FromInstance(player).AsSingle();
            return player;
        }
    }
}