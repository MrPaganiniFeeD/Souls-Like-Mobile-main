namespace PlayerLogic.States.State
{
    public interface IMoveStatePayloaded : IPlayerStatePayloaded
    {
        public float Speed { get; }
    }
}