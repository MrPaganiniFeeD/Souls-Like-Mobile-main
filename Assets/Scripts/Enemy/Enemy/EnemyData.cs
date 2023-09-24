using System.Collections.Generic;
using DefaultNamespace.Enemy.Factory;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster/Create New Monster", order = 51)]

public class EnemyData : ScriptableObject
{
    public string Name;
    public int Health;
    public MonsterTypeId MonsterTypeId;
    public List<EnemyAttackData> AttackDatas;

    public AudioClip TakeDamageAudioClip;
    public AudioClip DeathAudioClip;
}


