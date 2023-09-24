using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public interface IObservableTransform
{
    event Action<Transform> OnChangePosition;
    Transform GetTransform();
}
