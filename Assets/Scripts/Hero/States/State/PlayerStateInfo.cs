using System;
using System.Collections.Generic;
using Hero.State;
using UnityEngine;

namespace Hero.States.State
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