using System;
using System.Collections.Generic;
using Infrastructure.AssetsManagement;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using PlayerLogic.Stats;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public event Action CreatedPlayer;
        private readonly IAssets _assets;

        public List<ISavedProgress> ProgressesWriter { get; } = new List<ISavedProgress>();
        public List<ISavedProgressReader> ProgressesReader { get; } = new List<ISavedProgressReader>();
        
        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreatePlayer(GameObject at)
        {
            CreatedPlayer?.Invoke();
            return InstantiateRegister(AssetsPath.PlayerPath, at.transform.position);
        }

        public GameObject CreateScreenLoading() => 
            InstantiateRegisterNonZenject(AssetsPath.SceneCurtainPath);

        public GameObject CreateHud() => 
            InstantiateRegister(AssetsPath.HudPath);

        public GameObject CreateLockOnUI() =>
            InstantiateRegister(AssetsPath.LockOnUIPath);
        public IStatsProvider CreateStatsProvider() => 
            new ClassStats(ClassType.Warrior);

        public void CleanUp()
        {
            ProgressesWriter.Clear();
            ProgressesReader.Clear();
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress savedProgress)
                ProgressesWriter.Add(savedProgress);

            ProgressesReader.Add(progressReader);
        }

        public ISoundService CreateSoundService()
        {
            var gameObject = _assets.InstantiateNonZenject(AssetsPath.MusicPath);
            var soundService = gameObject.GetComponent<ISoundService>();
            RegisterService(gameObject);
            return soundService;
        }

        private GameObject InstantiateRegister(string namePrefab)
        {
            GameObject prefab = _assets.Instantiate(namePrefab);
            RegisterService(prefab);
            return prefab;
        }

        private GameObject InstantiateRegisterNonZenject(string namePrefab)
        {
            GameObject prefab = _assets.InstantiateNonZenject(namePrefab);
            RegisterService(prefab);
            return prefab;
        }

        private GameObject InstantiateRegisterNonZenject(string namePrefab, Vector3 position)
        {
            GameObject prefab = _assets.InstantiateNonZenject(namePrefab, position);
            RegisterService(prefab);
            return prefab;
        }

        private void RegisterService(GameObject prefab)
        {
            foreach (ISavedProgressReader progressReader in
                prefab.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private GameObject InstantiateRegister(string namePrefab, Vector3 at)
        {
            GameObject prefab = _assets.Instantiate(namePrefab, at);
            RegisterService(prefab);
            return prefab;
        }
    }
}