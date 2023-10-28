using System;

namespace Hero.Stats
{
    [Serializable]
    public class ItemStat : Stat
    {
        public ItemStat(string name) : base(name){}
        public ItemStat(string name, int baseValue, int maxValue) : base(name, baseValue, maxValue){}
        public ItemStat(string name,int baseValue) : base(name,baseValue){}
        public ItemStat() : base() {}
    }
    [Serializable]
    public class HealthItemStat : ItemStat
    {
        public HealthItemStat() : base()
        {
            Name = "Health";
        }
    }
    [Serializable]
    public class DamageItemStat : ItemStat
    {
        public DamageItemStat() : base()
        {
            Name = "Damage";
        }
    }
    [Serializable]
    public class StaminaItemStat : ItemStat
    {
        public StaminaItemStat() : base()
        {
            Name = "Stamina";
        }
    }
    [Serializable]
    public class ManaItemStat : ItemStat
    {
        public ManaItemStat() : base()
        {
            Name = "Mana";
        }
    }
    [Serializable]
    public class ProtectionItemStat : ItemStat
    {
        public ProtectionItemStat() : base()
        {
            Name = "Protection";
        }
    }
    [Serializable]
    public class IntelligenceItemStat : ItemStat
    {
        public IntelligenceItemStat() : base()
        {
            Name = "Intelligence";
        }
    }
}
