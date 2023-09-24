using Data;
using Data.Extensions;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "ProgressKey";
        
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        private readonly AllServices _allServices;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory,
            AllServices allServices)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
            _allServices = allServices;
        }

        public void SaveProgress()
        {
            foreach(ISavedProgress progressWriter in _gameFactory.ProgressesWriter)
                progressWriter.UpdateProgress(_progressService.PlayerProgress);
            
            foreach (ISavedProgress progressWriter in _allServices.SavedProgressesServices)
                progressWriter.UpdateProgress(_progressService.PlayerProgress);

            PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();

        public void Update()
        {
            
        }
    }
}