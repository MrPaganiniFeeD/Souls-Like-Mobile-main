using System;
using PlayerLogic.Stats;
using UnityEngine;

namespace DefaultNamespace.Bot.Stats
{
    [Serializable]
    public class EnemyStats : IStats
    {
        public Stat Health => _health;
        [SerializeField] private EnemyStat _health;

        public Stat Damage => _damage;
        [SerializeField] private EnemyStat _damage;

        public Stat Protection => _protection;
        [SerializeField] private EnemyStat _protection;
        
        public void ShowInfoStats()
        {
            Debug.Log($"{Health.Name} - {Health.Value}" +
                      $" {Damage.Name} - {Damage.Value} " +
                      $" {Protection.Name} - {Protection.Value}");
        }
    }
}