using System;
using PlayerLogic.Stats;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public KillData KillData;
        [FormerlySerializedAs("PlayerState")] public PlayerStateData playerStateData;
        public InventoryData InventoryData; 

        public PlayerProgress(string initialLevel, IStatsProvider statsProvider)
        {
            WorldData = new WorldData(initialLevel);
            playerStateData = new PlayerStateData(statsProvider);
            KillData = new KillData();
        }
    }
}