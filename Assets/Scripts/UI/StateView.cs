using PlayerLogic.States;
using PlayerLogic.States.State;
using PlayerLogic.States.StateMachine;
using TMPro;
using UnityEngine;

public class StateView : MonoBehaviour
{
    private Player _player;
    private PlayerStateMachine _playerStateMachine;
    [SerializeField] private TMP_Text _text;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _playerStateMachine = _player.GetComponent<PlayerStateMachine>();
        _playerStateMachine.UpdateCurrentState += OnUpdateCurrentState;
    }

    private void OnUpdateCurrentState(IState state)
    {
        if (state is AttackState) _text.SetText("AttackState");
        if (state is PlayerMoveState) _text.SetText("MoveState");
        if (state is IdleEnemyState) _text.SetText("IdleState");
    }

    private void OnDestroy()
    {
        _playerStateMachine.UpdateCurrentState -= OnUpdateCurrentState;
    }
}
