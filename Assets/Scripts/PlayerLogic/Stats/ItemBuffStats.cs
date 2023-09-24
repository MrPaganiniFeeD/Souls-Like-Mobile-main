using System;
using System.Collections;
using System.Collections.Generic;
using PlayerLogic.Stats;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ItemBuffStats : IStats
{
    [SerializeField] private ItemStat _healthBuff;
    [SerializeField] private ItemStat _damageBuff;
    [SerializeField] private ItemStat _staminaBuff;
    [SerializeField] private ItemStat _manaBuff;
    [SerializeField] private ItemStat _intelligenceBuff;
    [SerializeField] private ItemStat _protectionBuff;
    [SerializeField] private ItemStat _dexterityBuff;
    [Space]
    [SerializeField] private float _healthPercent;
    [SerializeField] private float _damagePercent;
    [SerializeField] private float _staminaPercent;
    [SerializeField] private float _manaPercent;
    [SerializeField] private float _intelligencePercent;
    [SerializeField] private float _protectionPercent;
    [SerializeField] private float _dectertityPercent;
    


    public Stat Health => _healthBuff;
    public Stat Damage => _damageBuff;
    public Stat Stamina => _staminaBuff;
    public Stat Mana => _manaBuff;
    public Stat Intelligence => _intelligenceBuff;
    public Stat Protection => _protectionBuff;
    public Stat Dexterity => _dexterityBuff;
    
    
    public float HealthPercent => _healthPercent;
    public float DamagePercent => _damagePercent;
    public float StaminaPercent => _staminaPercent;
    public float ManaPercent => _manaPercent;
    public float IntelligencePercent => _intelligencePercent;
    public float ProtectionPercent => _protectionPercent;
    public float DexterityPercent => _dectertityPercent;
    
    public void ShowInfoStats()
    {
        Debug.Log($"Health {Health.Value}" +
                  $" Damage - {Damage.Value} " +
                  $" Stamina - {Stamina.Value} " +
                  $" Mana - {Mana.Value}" +
                  $" Intelligence - {Intelligence.Value}" +
                  $" Protection - {Protection.Value}" +
                  $" Dexterity - {Dexterity.Value}");
    }
    
    
}
