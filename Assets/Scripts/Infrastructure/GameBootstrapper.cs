using DefaultNamespace.Infrastructure;
using Infrastructure.DI;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapper : MonoInstaller, ICoroutineRunner
    {
        private Game _game;
        private AllServices _allServices;

        public override void InstallBindings()
        {
            Debug.Log("GameBootstrapper");
            _allServices = new AllServices(Container);
            _game = new Game(_allServices, DiContainerSceneRef.Container);
            _game.StateMachine.Enter<BootstrapGameState>();
        }

        private void Update() => 
            _allServices.Update();
    }
}