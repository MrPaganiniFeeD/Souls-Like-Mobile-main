using System;
using Hero.States.State;
using UnityEngine;


public class DamageDetection : MonoBehaviour, IDamageDetection, IDamageable
{
    public event Action<Rigidbody, int> TakingBodyDamage;
    public event Action<int> TakingDamage;
    public event Action ReceivedDamage;


    public void ApplyDamage(Rigidbody attachedRigidbody, int damage)
    {
        ReceivedDamage?.Invoke();
        TakingBodyDamage?.Invoke(attachedRigidbody, damage);
    }

    public void ApplyDamage(int damage)
    {
        Debug.Log("Hit");
        ReceivedDamage?.Invoke();
        TakingDamage?.Invoke(damage);
    }


}

public interface IDamageable
{
    void ApplyDamage(Rigidbody attachedRigidbody, int damage);
    void ApplyDamage(int damage);
}