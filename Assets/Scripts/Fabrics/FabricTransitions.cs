using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services;
using PlayerLogic.States.State;
using PlayerLogic.States.StateMachine;
using PlayerLogic.States.Transition;
using PlayerLogic.Stats;
using PlayerLogic.Transition;

namespace Fabrics
{
    public class FabricTransitions : IFabricTransition
    {
        private IInputService _inputService;
        private readonly PlayerStateMachine _playerStateMachine;
    
        private IDamageDetection _damageDetection;
        private PlayerStats _playerStats;
        private RollStateData _rollingData;

        public FabricTransitions(IInputService inputService,
            PlayerStateMachine playerStateMachine,
            RollStateData rollingData)
        {
            _inputService = inputService;
            _playerStateMachine = playerStateMachine;
            _rollingData = rollingData;
        }
        public ITransition CreateTransition(TypeTransitions type)
        {
            switch (type)
            {
                case TypeTransitions.Idle :
                    return new IdleTransition(_inputService,
                        _playerStateMachine);
                case TypeTransitions.Move :
                    return new MoveTransition(_inputService,
                        _playerStateMachine);
                case TypeTransitions.Attack :
                    return new AttackTransition(_playerStateMachine, 
                        _inputService);
                case TypeTransitions.Rollig: 
                    return new RollTransition(_playerStateMachine,
                        _playerStats,
                        _inputService,
                        _rollingData);
            }
            return new UnknownTransition();
        }
    
        public List<ITransition> CreatTransitions(IEnumerable<TypeTransitions> typeTransitions) => 
            typeTransitions.Select(CreateTransition).ToList();

    }
}
