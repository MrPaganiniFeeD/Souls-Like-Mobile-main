using UnityEngine;

namespace DefaultNamespace.Stats
{
    public class ItemStatsInfo : IStatsInfo
    {
        [SerializeField] private string _name;
        [SerializeField] private int _baseValue;
        
        
        public string Name { get; }
        public int BaseValue { get; }
        public int CurrentValue { get; }
    }
}