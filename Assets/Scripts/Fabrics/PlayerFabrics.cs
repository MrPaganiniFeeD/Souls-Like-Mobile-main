using Fabrics;
using Hero;
using Infrastructure.Services;
using Infrastructure.Services.Inventory;
using Infrastructure.Services.PersistentProgress;
using Hero.States.State;
using Hero.States.StateMachine;
using Hero.Stats;
using UnityEngine;
using Zenject;



public class PlayerFabrics : MonoBehaviour
{
    [SerializeField] private RollStateData _rollStateData;
    
    public IFabricState FabricState => _fabricState;
    public IFabricTransition FabricTransition => _fabricTransitions;
    
    
    private IInventoryService _inventoryService;
    private IInputService _inputService;
    private PlayerStats _playerStats;
    private IFabricState _fabricState;
    private IFabricTransition _fabricTransitions;
    private Player _player;
    private PlayerStateMachine _playerStateMachine;
    private PlayerStateAnimator _playerAnimator;
    private PlayerCamera _playerCamera;

    [Inject]
    public void Construct(IInventoryService inventoryService, 
        IInputService inputService, 
        IPersistentProgressService progressService)
    {
        _inventoryService = inventoryService;
        _inputService = inputService;
        _playerStats = progressService.PlayerProgress.playerStateData.PlayerStats;
        _playerCamera = Camera.main.GetComponentInParent<PlayerCamera>();
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        _playerAnimator = GetComponent<PlayerStateAnimator>();
        
        _fabricTransitions = new FabricTransitions(_inputService,
            _playerStateMachine,
            _rollStateData,
            _playerStats,
            GetComponent<IDamageDetection>());
        _fabricState = new FabricPlayerStates(_inputService,
            _inventoryService,
            _playerStats,
            _fabricTransitions,
            _player,
            _playerStateMachine,
            _playerAnimator,
            _playerCamera,
            _rollStateData);
    }
}