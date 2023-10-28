using System;
using System.Collections;
using System.Collections.Generic;
using Hero.Stats;
using TMPro;
using UnityEngine;

public class UIStatItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameStat;
    [SerializeField] private TMP_Text _value;

    private ItemBuffStats _itemBuffStats;

    
    public void Show(IItemBuffStats buffStat)
    {
        Debug.Log(buffStat.HealthBonus,              _nameStat);
        Debug.Log(buffStat.HealthPercentBonus,       _nameStat);
        Debug.Log(buffStat.DamageBonus,              _nameStat);
        Debug.Log(buffStat.DamagePercentBonus,       _nameStat);
        Debug.Log(buffStat.StaminaBonus,             _nameStat);
        Debug.Log(buffStat.StaminaPercentBonus,      _nameStat);
        Debug.Log(buffStat.ManaBonus,                _nameStat);
        Debug.Log(buffStat.ManaPercentBonus,         _nameStat);
        Debug.Log(buffStat.IntelligenceBonus,        _nameStat);
        Debug.Log(buffStat.IntelligencePercentBonus, _nameStat);
        Debug.Log(buffStat.ProtectionBonus,          _nameStat);
        Debug.Log(buffStat.ProtectionPercentBonus,   _nameStat);
        Debug.Log(buffStat.DexterityBonus,           _nameStat);
        Debug.Log(buffStat.DexterityPercentBonus,    _nameStat);

    }
    
    public void UpdateValue(Stat stat)
    {
        if(stat.Value > 0)
        {
            _nameStat.text = stat.Name;
            _value.text = "+" + stat.Value;
        }
        else if (stat.Value < 0)
        {
            _nameStat.text = stat.Name;
            _value.text = "-" + stat.Value;
        }
    }
    public void Clear()
    {
        _nameStat.text = "";
        _value.text = "";
    }
    
}
