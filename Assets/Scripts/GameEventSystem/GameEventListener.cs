using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;

    [SerializeField] private UnityEvent<Item> _actionsItem;
    [SerializeField] private UnityEvent<Item, IInventorySlot> _actionsItemAndSlot;

    private void OnEnable()
    {
        _gameEvent.AddListener(this);
    }
    private void OnDisable()
    {
        _gameEvent.RemoveListener(this);
    }
    public void EventRaised(Item item)
    {
        _actionsItem?.Invoke(item);
    }
    public void EventRaised(Item item , IInventorySlot slot)
    {
        _actionsItemAndSlot?.Invoke(item, slot);
    }


}
