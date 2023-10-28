using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;

public class CanvasCamera : MonoBehaviour, IService
{
    public Camera Camera;
    
    public void Awake()
    {
        Camera = GetComponent<Camera>();
    }
}
