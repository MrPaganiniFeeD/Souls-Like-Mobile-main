using System.Collections;
using UnityEngine;

public interface IMoveModule
{
    void MoveAlongACurveUsingCharacterController(Vector3 direction, float duration, float speed, AnimationCurve curve);

    void AddForce(Vector3 direction, ForceMode forceMode);
    void Rotate(Vector3 direction);
    void Rotate(Transform target);
}