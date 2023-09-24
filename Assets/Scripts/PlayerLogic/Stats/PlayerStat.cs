using System;

namespace PlayerLogic.Stats
{
    [Serializable]
    public class PlayerStat : Stat
    {
        public PlayerStat(string name) : base(name){}
        public PlayerStat(string name, int baseValue, int maxValue) : base(name ,baseValue, maxValue){}
        public PlayerStat(string name,int baseValue) : base(name,baseValue){}
        public PlayerStat() : base() {}

    }
}
