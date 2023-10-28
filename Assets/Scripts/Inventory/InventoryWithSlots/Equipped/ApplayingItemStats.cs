using Hero.Stats;

public class ApplyingItemStats : IApplyingItemStats
{
    private PlayerStats _playerStats;

    public ApplyingItemStats(PlayerStats playerStats)
    {
        _playerStats = playerStats;
    }

    public void Equip(ItemBuffStats itemBuffStats, object source)
    {
        AddBonus(_playerStats.Health,       StatModifierType.Flat, itemBuffStats.Health.Value, source);
        AddBonus(_playerStats.Damage,       StatModifierType.Flat, itemBuffStats.Damage.Value, source);
        AddBonus(_playerStats.Stamina,      StatModifierType.Flat, itemBuffStats.Stamina.Value, source);
        AddBonus(_playerStats.Mana,         StatModifierType.Flat, itemBuffStats.Mana.Value, source);
        AddBonus(_playerStats.Intelligence, StatModifierType.Flat, itemBuffStats.Intelligence.Value, source);
        AddBonus(_playerStats.Protection,   StatModifierType.Flat, itemBuffStats.Protection.Value, source);
        
        AddMaxValueBonus(_playerStats.Health, StatModifierType.PercentMult, itemBuffStats.HealthPercent, source);
        AddBonus(_playerStats.Damage,         StatModifierType.PercentMult, itemBuffStats.DamagePercent, source);
        AddBonus(_playerStats.Stamina,        StatModifierType.PercentMult, itemBuffStats.StaminaPercent, source);
        AddBonus(_playerStats.Mana,           StatModifierType.PercentMult, itemBuffStats.ManaPercent, source);
        AddBonus(_playerStats.Intelligence,   StatModifierType.PercentMult, itemBuffStats.IntelligencePercent, source);
        AddBonus(_playerStats.Protection,     StatModifierType.PercentMult, itemBuffStats.ProtectionPercent, source);

        
        
        _playerStats.ShowInfoStats();
    }

    public void UnEquip(object source)
    {
        _playerStats.Health.TryRemoveAllModifiersFromSource(source);
        _playerStats.Damage.TryRemoveAllModifiersFromSource(source);
        _playerStats.Stamina.TryRemoveAllModifiersFromSource(source);
        _playerStats.Mana.TryRemoveAllModifiersFromSource(source);
        _playerStats.Intelligence.TryRemoveAllModifiersFromSource(source);
        _playerStats.Protection.TryRemoveAllModifiersFromSource(source);
        _playerStats.Dexterity.TryRemoveAllModifiersFromSource(source);
        
        _playerStats.ShowInfoStats();
    }
    
    private void AddMaxValueBonus(Stat playerStat, StatModifierType statModifierType, float itemStat, object source)
    {
        AddBonus(playerStat, statModifierType, itemStat, source);
        playerStat.SetMaxValue();
    }

    private void AddBonus(Stat playerStat, StatModifierType statModifierType, float bonus, object source)
    {
        if (statModifierType == StatModifierType.Flat)
                AddModifier(playerStat, StatModifierType.Flat, (int)bonus , source);
        
        else if(statModifierType == StatModifierType.PercentMult)
                AddModifier(playerStat, StatModifierType.PercentMult, bonus, source);
    }

    private void AddModifier(Stat playerStat, StatModifierType statModifierType, float bonus, object source)
    {
        if (bonus != 0)
            playerStat.AddModifier(new StatModifier(bonus, statModifierType, source));
    }
}
