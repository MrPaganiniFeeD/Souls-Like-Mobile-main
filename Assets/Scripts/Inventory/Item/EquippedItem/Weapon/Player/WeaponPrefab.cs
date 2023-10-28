using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponPrefab : MonoBehaviour
{
    public event Action<IDamageable> CollidedWithEnemy;
    
    [FormerlySerializedAs("_collider")] [SerializeField] private Collider[] _colliders;
    [SerializeField] private ColliderDetection[] _colliderDetection;

    private void Awake()
    {
        _colliders = GetComponentsInChildren<Collider>(); 
        _colliderDetection = GetComponentsInChildren<ColliderDetection>();
        DisableCollider();
        foreach (var colliderDetection in _colliderDetection) 
            colliderDetection.DamageableObjectDetection += OnColliderDetection;
    }
    public void EnableCollider()
    {
        foreach (var collider in _colliders) 
            collider.enabled = true;
    }

    public void DisableCollider()
    {
        foreach (var collider in _colliders)
            collider.enabled = false;
    }

    private void OnColliderDetection(IDamageable damageable) => 
        CollidedWithEnemy?.Invoke(damageable);

    private void OnDestroy()
    {
        foreach (var colliderDetection in _colliderDetection) 
            colliderDetection.DamageableObjectDetection -= OnColliderDetection;
    }
}