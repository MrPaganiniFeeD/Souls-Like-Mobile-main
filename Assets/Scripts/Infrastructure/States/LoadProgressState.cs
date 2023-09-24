using Data;
using DefaultNamespace.Infrastructure;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using PlayerLogic.Stats;

namespace Infrastructure.States
{
    public class LoadProgressGameState : IGameState
    {
        private const string InitialLevel = "Main";
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressGameState(GameStateMachine stateMachine, 
            IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInit();
            _stateMachine.Enter<LoadLevelGameState, string>(_progressService.PlayerProgress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }
        
        private void LoadProgressOrInit() =>
            _progressService.PlayerProgress =
                _saveLoadService.LoadProgress() 
                ??
                NewProgress();

        private PlayerProgress NewProgress() => 
            _progressService.PlayerProgress = new PlayerProgress(InitialLevel,
                new ClassStats(ClassType.Warrior));
    }
}