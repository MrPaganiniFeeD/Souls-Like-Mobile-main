using System;
using DefaultNamespace.UI.Scene;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.States
{
    public class StartGameState : IGameState, IDisposable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        private readonly SceneLoader _sceneLoader;

        private LoadingCurtain _loadingCurtain;
        private StartMenu _startMenu;
        private const string StartMenuScene = "Start";
        private const string StartMenuPrefab = "Prefabs/UI/StartMenu";

        public StartGameState(GameStateMachine gameStateMachine,
            IPersistentProgressService progressService,
            IGameFactory gameFactory,
            SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _gameFactory = gameFactory;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _loadingCurtain = InstantiateLoadingCurtain();
            _loadingCurtain.Show();
            _sceneLoader.ChangeProgress += OnChangeProgress;
            _sceneLoader.Load(StartMenuScene, OnLoaded);
        }

        public void Exit() => 
            _startMenu.StartedGame -= OnStartedGame;

        private void OnLoaded()
        {
            _startMenu = _gameFactory.InstantiateRegister(StartMenuPrefab).GetComponent<StartMenu>();
            _startMenu.StartedGame += OnStartedGame;
            
            _loadingCurtain.Hide();
            _sceneLoader.ChangeProgress -= OnChangeProgress;
        }

        private void OnChangeProgress(float value) => 
            _loadingCurtain.UpdateProgress(value);
        
        private LoadingCurtain InstantiateLoadingCurtain()
        {
            GameObject loadingCurtainPrefab = _gameFactory.CreateScreenLoading();
            return loadingCurtainPrefab.GetComponent<LoadingCurtain>();
        }

        private void OnStartedGame()
        {
            _gameStateMachine.Enter<LoadLevelGameState, string>(_progressService
                .PlayerProgress
                .WorldData
                .PositionOnLevel
                .Level);
        }

        public void Dispose()
        {
            _startMenu.StartedGame -= OnStartedGame;
        }
    }
}