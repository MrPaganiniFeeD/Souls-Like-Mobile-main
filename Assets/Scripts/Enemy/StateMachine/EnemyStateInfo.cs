using System;
using System.Collections.Generic;

[Serializable]
internal class EnemyStateInfo
{
    public TypeEnemyState EnemyState;
    public List<TypeEnemyTransition> TypeEnemyTransitions;
    public List<EnemyState> EnemyStates;
}