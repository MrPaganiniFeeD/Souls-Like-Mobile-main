﻿using System;
using Hero.Stats;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class PlayerStateData
    {
        public PlayerStats PlayerStats;

        public PlayerStateData(IStatsProvider statsProvider)
        {
            PlayerStats = statsProvider.GetStats();
            PlayerStats.ShowInfoStats();
        }
    }
}