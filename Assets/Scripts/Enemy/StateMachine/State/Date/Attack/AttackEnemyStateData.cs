using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = System.Numerics.Vector3;

[Serializable]
public class AttackEnemyStateData : StateDate
{
    public List<EnemyAttackData> Attacks;
    public AttackEnemyDataTransition AttackEnemyDataTransition;
    public Transform TransformCollider;
    [FormerlySerializedAs("X")] [Header("ColliderSize")] public float ColliderSizeX;
    [FormerlySerializedAs("Y")] [Header("ColliderSize")] public float ColliderSizeY;
    [FormerlySerializedAs("ColliderSizeY")] [FormerlySerializedAs("Z")] [Header("ColliderSize")] public float ColliderSizeZ;
}