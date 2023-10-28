using System;
using System.Collections.Generic;
using DefaultNamespace.Stats;
using Hero.Stats;
using UnityEngine;

[Serializable]
public class ItemBuffStats : IStats
{
    [SerializeField] private HealthItemStat _healthBuff;
    [SerializeField] private DamageItemStat _damageBuff;
    [SerializeField] private StaminaItemStat _staminaBuff;
    [SerializeField] private ManaItemStat _manaBuff;
    [SerializeField] private IntelligenceItemStat _intelligenceBuff;
    [SerializeField] private ProtectionItemStat _protectionBuff;
    [Space] 
    [SerializeField] private float _healthPercent;
    [SerializeField] private float _damagePercent;
    [SerializeField] private float _staminaPercent;
    [SerializeField] private float _manaPercent;
    [SerializeField] private float _intelligencePercent;
    [SerializeField] private float _protectionPercent;




    public Stat Health => _healthBuff;
    public Stat Damage => _damageBuff;
    public Stat Stamina => _staminaBuff;
    public Stat Mana => _manaBuff;
    public Stat Intelligence => _intelligenceBuff;
    public Stat Protection => _protectionBuff;



    public float HealthPercent => _healthPercent;
    public float DamagePercent => _damagePercent;
    public float StaminaPercent => _staminaPercent;
    public float ManaPercent => _manaPercent;
    public float IntelligencePercent => _intelligencePercent;
    public float ProtectionPercent => _protectionPercent;


    public void ShowInfoStats()
    {
        Debug.Log($"Health {Health.Value}" +
                  $" Damage - {Damage.Value} " +
                  $" Stamina - {Stamina.Value} " +
                  $" Mana - {Mana.Value}" +
                  $" Intelligence - {Intelligence.Value}" +
                  $" Protection - {Protection.Value}");
    }

    public List<Stat> GetAllStats() => new List<Stat>()
        { Health, Mana, Stamina, Damage, Intelligence, Protection };


}
