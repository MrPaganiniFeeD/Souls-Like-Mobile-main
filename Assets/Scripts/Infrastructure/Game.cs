using DefaultNamespace.Infrastructure;
using Infrastructure.Services;
using Infrastructure.States;
using Zenject;

namespace Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;
        
        public Game(AllServices allServices, DiContainer diContainer)
        {
            StateMachine = new GameStateMachine(new SceneLoader(), allServices, diContainer);
        }
    }
}