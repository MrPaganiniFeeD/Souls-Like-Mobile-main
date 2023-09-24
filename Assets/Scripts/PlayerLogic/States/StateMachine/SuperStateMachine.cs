using System;
using PlayerLogic.States.StateMachine;

namespace PlayerLogic.State.StateMachine
{
    public class SuperPlayerStateMachine : PlayerStateMachine
    {
        /*
        private List<SuperStateInfo> _stateInfos;
        private Dictionary<Enum, SuperState> _allStates;

        private SuperState _currentSuperState;
    
        private IFabricState _fabricPlayerStates;
        private bool _isBlockedSwitch;

        public SuperPlayerStateMachine(List<SuperStateInfo> stateInfos, IFabricState fabricState) : base(fabricState)
        {
            _stateInfos = stateInfos;
            _fabricPlayerStates = fabricState;
        }
        public override void InitStates()
        {
            _allStates = GetCreationStates();
            _currentSuperState = _allStates[_stateInfos[0].SuperState];
            _currentSuperState.Enter();
            _isBlockedSwitch = false;
        }
        public override void SwitchState(Enum typeState)
        {
            if (_isBlockedSwitch)
                return;
            if (!_currentSuperState.IsLooping &&
                Equals(_allStates
                    .FirstOrDefault(x => x.Value == _currentSuperState).Key, typeState))
                return;

            SetNewState(typeState);
        }
    
        public override void Update() => 
            _currentSuperState?.Update();

        public override void ClearState()
        {
            _currentSuperState.Exit();
            SetBlockedSwitch(false);
            _currentSuperState = null;
        }

        protected override void SetNewState(Enum typeState)
        {
            SuperState newGameState = GetState(typeState);
            _currentSuperState.Exit();
            SetBlockedSwitch(newGameState.IsBlocked);
            newGameState.Enter();
            _currentSuperState = newGameState;
        }

        private new void SetBlockedSwitch(bool flag) => 
            _isBlockedSwitch = flag;

        private SuperState GetCreationSuperState(ISuperStateInfo stateInfo) => 
            _fabricPlayerStates.CreateSuperState(stateInfo, new BaseStateMachine(stateInfo.ChildrenStateInfos, _fabricPlayerStates));

        private Dictionary<Enum, SuperState> GetCreationStates() => 
            _stateInfos.ToDictionary(stateInfo => stateInfo.SuperState, GetCreationSuperState);

        private SuperState GetState(Enum typeState) => 
            _allStates[typeState];
            */

        public SuperPlayerStateMachine(IFabricState fabricState) 
        {
        }

        public void InitStates()
        {
            throw new NotImplementedException();
        }

        public void SwitchState(Enum typeState)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void ClearState()
        {
            throw new NotImplementedException();
        }

        protected void SetNewState(Enum typeState)
        {
            throw new NotImplementedException();
        }
    }
}