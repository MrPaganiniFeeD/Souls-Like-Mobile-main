using System;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    public event Action<IDamageable> DamageableObjectDetection;

    private IDamageable _currentDamageableCollider;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IDamageable damageable))
            DamageableObjectDetection?.Invoke(damageable);
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
            _currentDamageableCollider = damageable;
        else
            _currentDamageableCollider = null;
    }

    public IDamageable GetCurrentDamageable() => 
        _currentDamageableCollider;
}