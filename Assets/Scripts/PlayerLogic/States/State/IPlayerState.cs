namespace PlayerLogic.States.State
{
    public interface IPlayerState<TPayloaded> where TPayloaded : IPlayerStatePayloaded 
    {
        void Enter(TPayloaded payloaded);
    }
}