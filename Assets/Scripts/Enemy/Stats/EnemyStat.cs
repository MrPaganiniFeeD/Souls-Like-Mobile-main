using System;
using PlayerLogic.Stats;
using UnityEngine;

namespace DefaultNamespace.Bot.Stats
{
    [Serializable]
    public class EnemyStat : Stat
    {
        public EnemyStat(string name) : base(name){}
        public EnemyStat(string name, int baseValue, int maxValue) : base(name ,baseValue, maxValue){}
        public EnemyStat(string name,int baseValue) : base(name,baseValue){}
        public EnemyStat() : base() {}
    }
}