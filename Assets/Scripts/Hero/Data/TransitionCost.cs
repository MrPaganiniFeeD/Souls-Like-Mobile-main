using System;
using UnityEngine;

[Serializable]
public struct TransitionCost : ITransitionCost
{
    [SerializeField] private int _manaCost;
    [SerializeField] private int _staminaCost;

    public int Mana => _manaCost;
    public int Stamina => _staminaCost;

}
