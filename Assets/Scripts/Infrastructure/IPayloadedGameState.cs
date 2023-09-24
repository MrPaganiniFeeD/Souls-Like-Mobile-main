using DefaultNamespace.Infrastructure;

namespace Infrastructure
{
    public interface IPayloadedGameState<TPayload> : IExitableGameState
    {
        void Enter(TPayload sceneName);
    }
}