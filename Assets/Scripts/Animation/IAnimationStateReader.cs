using System;

namespace DefaultNarmespace.Player.AnimatorReporter
{
    public interface IAnimationStateReader
    {
        event Action ExitState;
        event Action EnterState;

        void EnteredState(int hashAnimation);
        void ExitedState(int hashAnimation);
    }
}