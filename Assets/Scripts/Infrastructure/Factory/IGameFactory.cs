using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Hero.Stats;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        event Action CreatedPlayer;
        List<ISavedProgress> ProgressesWriter { get; }
        List<ISavedProgressReader> ProgressesReader { get; }
        GameObject CreatePlayer(GameObject at);
        GameObject CreateScreenLoading();
        GameObject CreateHud();
        GameObject CreateLockOnUI();
        GameObject CreateCanvasCamera();
        IStatsProvider CreateStatsProvider();
        void CleanUp();
        void Register(ISavedProgressReader savedProgressService);
        ISoundService CreateSoundService();
        public GameObject InstantiateRegister(string namePrefab);

        public GameObject InstantiateRegister(string namePrefab, Quaternion quaternion, Vector3 position,
            Transform parent);

    }
}