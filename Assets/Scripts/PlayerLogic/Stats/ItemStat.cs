using System;

namespace PlayerLogic.Stats
{
    [Serializable]
    public class ItemStat : Stat
    {
        public ItemStat(string name) : base(name){}
        public ItemStat(string name, int baseValue, int maxValue) : base(name, baseValue, maxValue){}
        public ItemStat(string name,int baseValue) : base(name,baseValue){}
        public ItemStat() : base() {}
    }
}
