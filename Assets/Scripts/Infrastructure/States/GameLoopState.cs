using DefaultNamespace.Infrastructure;
using Infrastructure.Services;

namespace Infrastructure.States
{
    public class GameLoopState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _allServices;

        public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _allServices = allServices;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
        }
    }
}