using DefaultNamespace.Infrastructure;

namespace Infrastructure
{
    public interface IGameState : IExitableGameState
    {
        void Enter();
    }
}