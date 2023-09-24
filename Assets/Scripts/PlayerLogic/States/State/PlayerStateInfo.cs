using System;
using System.Collections.Generic;
using PlayerLogic.State;
using UnityEngine;

namespace PlayerLogic.States.State
{
    [Serializable]
    public class PlayerStateInfo : IStateInfo<TypePlayerState, TypeTransitions>
    {
        [SerializeField] private TypePlayerState _state;
        [SerializeField] private List<TypeTransitions> _transitions;

        public TypePlayerState State => _state;
        public List<TypeTransitions> Transitions => _transitions;

    }
}