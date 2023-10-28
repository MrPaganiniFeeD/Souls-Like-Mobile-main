using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services;
using Hero.States.State;
using Hero.States.StateMachine;
using Hero.States.Transition;
using Hero.Stats;
using Hero.Transition;

namespace Fabrics
{
    public class FabricTransitions : IFabricTransition
    {
        private IInputService _inputService;
        private readonly PlayerStateMachine _playerStateMachine;
        
        private PlayerStats _playerStats;
        private readonly IDamageDetection _damageDetection;
        private RollStateData _rollingData;

        public FabricTransitions(IInputService inputService,
            PlayerStateMachine playerStateMachine,
            RollStateData rollingData,
            PlayerStats playerStats,
            IDamageDetection damageDetection)
        {
            _inputService = inputService;
            _playerStateMachine = playerStateMachine;
            _rollingData = rollingData;
            _playerStats = playerStats;
            _damageDetection = damageDetection;
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
                case TypeTransitions.TakeDamage:
                    return new TakeDamageTransition(_playerStateMachine,
                        _damageDetection);
            }
            return new UnknownTransition();
        }
    
        public List<ITransition> CreatTransitions(IEnumerable<TypeTransitions> typeTransitions) => 
            typeTransitions.Select(CreateTransition).ToList();

    }
}

