using System;
using UnityEngine;

public class Lockable : MonoBehaviour
{
    public event Action Disable;
    
    [SerializeField] public Transform LockOnTransform;
    
    public bool Enable { get; private set; }

    private void Awake()
    {
        Enable = true;
    }

    public void SetState(bool state)
    {
        Enable = state;
        Debug.Log(Enable);
        Disable?.Invoke();
    }
}
