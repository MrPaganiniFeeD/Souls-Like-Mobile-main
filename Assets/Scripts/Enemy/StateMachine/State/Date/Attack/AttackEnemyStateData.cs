using System;
using System.Collections.Generic;

[Serializable]
public class AttackEnemyStateData : StateDate
{
    public List<EnemyAttackData> Attacks;
    public AttackEnemyDataTransition AttackEnemyDataTransition;
}