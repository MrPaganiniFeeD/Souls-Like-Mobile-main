using System;
using DefaultNamespace.Stats;
using UnityEngine;

namespace Hero.Stats
{
    [Serializable]
    public class PlayerStats : IStats
    {
        public Stat Health => _health;
        [SerializeField] private PlayerStat _health;
        public Stat Damage => _damage;
        [SerializeField] private PlayerStat _damage;
        public Stamina Stamina => _stamina;
        [SerializeField] private Stamina _stamina;
        public Mana Mana => _mana;
        [SerializeField] private Mana _mana;

        public Stat Intelligence => _intelligence;
        [SerializeField] private PlayerStat _intelligence;
    
        public Stat Protection => _protection;
        [SerializeField] private PlayerStat _protection;

        public Stat Dexterity => _dexterity;
        [SerializeField] private PlayerStat _dexterity;

        public PlayerStats(PlayerStat health,
            PlayerStat damage,
            Stamina stamina,
            Mana mana,
            PlayerStat intelligence,
            PlayerStat protection,
            PlayerStat dexterity)
        {
            _health = health;
            _damage = damage;
            _stamina = stamina;
            _mana = mana;
            _intelligence = intelligence;
            _protection = protection;
            _dexterity = dexterity;
        }
    
        public void ShowInfoStats()
        {
            Debug.Log(  "Player Stats -- " + $"{Health.Name} - {Health.Value}" +
                      $" {Damage.Name} - {Damage.Value} " +
                      $" {Stamina.Name} - {Stamina.Value} " +
                      $" {Mana.Name} - {Mana.Value}" +
                      $" {Intelligence.Name} - {Intelligence.Value}" +
                      $" {Protection.Name} - {Protection.Value}" +
                      $" {Dexterity.Name} - {Dexterity.Value}");
        }
    }
}
