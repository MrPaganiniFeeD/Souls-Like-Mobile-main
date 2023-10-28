using System;
using System.Collections.Generic;
using Hero.States.State;

public interface IFabricState
{
    Dictionary<Type, IState> CreateStates(IEnumerable<PlayerStateInfo> stateInfos);
}
