using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDetection
{
    event Action<Rigidbody,int> TakingBodyDamage;
    event Action<int> TakingDamage;
    event Action ReceivedDamage;
    
    void ApplyDamage(Rigidbody attachedRigidbody, int damage);
    void ApplyDamage(int damage);

}
