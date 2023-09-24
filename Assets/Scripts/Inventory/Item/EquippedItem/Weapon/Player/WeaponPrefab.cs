using System;
using UnityEngine;

public class WeaponPrefab : MonoBehaviour
{
    public event Action<IDamageable> CollidedWithEnemy;
    
    [SerializeField] private Collider _collider;
    [SerializeField] private ColliderDetection[] _colliderDetection;

    private void Awake()
    {
        _collider = GetComponentInChildren<Collider>();
        _colliderDetection = GetComponentsInChildren<ColliderDetection>();
        DisableCollider();
        foreach (var colliderDetection in _colliderDetection) 
            colliderDetection.DamageableObjectDetection += OnColliderDetection;
    }

    public void SetColliders(Collider[] colliders) =>
        new ArgumentException();

    public void SetColliderDetections(ColliderDetection[] colliderDetections) => 
        _colliderDetection = colliderDetections;
    

    public void EnableCollider() => 
        _collider.enabled = true;

    public void DisableCollider() => 
        _collider.enabled = false;

    private void OnColliderDetection(IDamageable damageable) => 
        CollidedWithEnemy?.Invoke(damageable);

    private void OnDestroy()
    {
        foreach (var colliderDetection in _colliderDetection) 
            colliderDetection.DamageableObjectDetection -= OnColliderDetection;
    }
}