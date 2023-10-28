using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemBuffStats
{
    int HealthBonus { get; }
    int DamageBonus { get; }
    int StaminaBonus { get; }
    int ManaBonus { get; }
    int IntelligenceBonus { get; }
    int ProtectionBonus { get; }
    int DexterityBonus { get; }
    
    float HealthPercentBonus { get; }
    float DamagePercentBonus { get; }
    float StaminaPercentBonus { get; }
    float ManaPercentBonus { get; }
    float IntelligencePercentBonus { get; }
    float ProtectionPercentBonus { get; }
    float DexterityPercentBonus { get; }

    int MaxHealthBonus { get; }
    int MaxStaminaBonus { get; }
    int MaxManaBonus { get; }
}
