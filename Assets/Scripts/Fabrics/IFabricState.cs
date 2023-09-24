using System;
using System.Collections.Generic;
using PlayerLogic.States.State;

public interface IFabricState
{
    Dictionary<Type, IState> CreateStates(IEnumerable<PlayerStateInfo> stateInfos);
}
