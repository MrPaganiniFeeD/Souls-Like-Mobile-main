using System.Collections;
using System.Collections.Generic;
using PlayerLogic.Stats;
using UnityEngine;

public interface IStats
{
    Stat Health { get; }
    Stat Damage { get; }
    Stat Protection { get; }

    public void ShowInfoStats();
}
