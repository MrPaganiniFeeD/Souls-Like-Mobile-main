using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameEvent", menuName = "SO/Create Game Event", order = 51)]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    public void AddListener(GameEventListener listener)
    {
        _listeners.Add(listener);
    }
    public void RemoveListener(GameEventListener listener)
    {
        _listeners.Remove(listener);
    }
    public void Init(Item item)
    {
        foreach (var listener in _listeners)
        {
            listener.EventRaised(item);
        }
    }
    public void Init(Item item, IInventorySlot slot)
    {
        foreach (var listener in _listeners)
        {
            listener.EventRaised(item);
        }
    }
}
