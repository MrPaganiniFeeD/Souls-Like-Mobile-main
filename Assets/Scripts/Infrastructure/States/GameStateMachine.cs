using System;
using System.Collections.Generic;
using Infrastructure.AssetsManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Zenject;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableGameState> _states;
        private IExitableGameState _currentState;

        public GameStateMachine(SceneLoader sceneLoader, 
            AllServices allServices,
            DiContainer diContainer)
        {
            _states = new Dictionary<Type, IExitableGameState>
            {
                [typeof(BootstrapGameState)] = new BootstrapGameState(this, sceneLoader, allServices),
                [typeof(LoadProgressGameState)] = new LoadProgressGameState(this,
                        allServices.Single<IPersistentProgressService>(),
                        allServices.Single<ISaveLoadService>()),
                [typeof(LoadLevelGameState)] = new LoadLevelGameState(this, sceneLoader, diContainer,
                    allServices.Single<IGameFactory>(),
                    allServices.Single<IPersistentProgressService>(),
                    allServices.Single<IInputService>(),
                    allServices.Single<IAssets>()),
                [typeof(GameLoopState)] = new GameLoopState(this, sceneLoader, allServices)
            };
        }
        
        public void Enter<TState>() where TState : class, IGameState
        {
            TState newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedGameState<TPayload>
        {
            IPayloadedGameState<TPayload> newGameState = ChangeState<TState>();
            newGameState.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableGameState
        {
            TState newState = GetState<TState>();
            _currentState?.Exit();
            _currentState = newState;
            return newState;
        }

        private TState GetState<TState>() where TState : class, IExitableGameState =>
            _states[typeof(TState)] as TState;
    }
}