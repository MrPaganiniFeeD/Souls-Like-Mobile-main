using System;
using Hero.Stats;

namespace DefaultNamespace.Stats
{
    [Serializable]
    public class Mana : Stat, IUsingStat
    {
        public Mana(string name, int baseValue, int maxValue) :
            base(name, baseValue, maxValue)
        {
        }
        public Mana(string name) : base(name){}
        public Mana(string name,int baseValue) : base(name,baseValue){}
        
        
        public bool TryUse(int costValue)
        {
            if (Value - costValue >= 0)
            {
                Value -= costValue;
                return true;
            }
            return false;
        }
        public bool CheckValue(int value)
        {
            return Value - value >= 0;
        }
    }
}