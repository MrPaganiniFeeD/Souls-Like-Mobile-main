using System;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    public event Action<IDamageable> DamageableObjectDetection;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IDamageable damageable))
            DamageableObjectDetection?.Invoke(damageable);
    }
}