using System.Collections;
using Infrastructure;
using UnityEngine;

public class BaseMoveModule : IMoveModule
{
    private readonly Transform _transform;
    private readonly Camera _camera;
    private readonly Rigidbody _rigidbody;
    private CharacterController _characterController;
    private readonly MonoBehaviour _monoBehaviour;

    public BaseMoveModule(Transform transform,
        Camera camera,
        CharacterController characterController,
        MonoBehaviour monoBehaviour)
    {
        _transform = transform;
        _camera = camera;
        _characterController = characterController;
        _monoBehaviour = monoBehaviour;
    }
    public BaseMoveModule(Transform transform,
        Camera camera,
        Rigidbody rigidbody,
        MonoBehaviour monoBehaviour)
    {
        _transform = transform;
        _camera = camera;
        _rigidbody = rigidbody;
        _monoBehaviour = monoBehaviour;
    }
    public void MoveAlongACurveUsingCharacterController(Vector3 direction, float duration, float speed, AnimationCurve curve)
    {
        _monoBehaviour.StopCoroutine(MoveOnCurve(direction ,duration, curve , speed));
        _monoBehaviour.StartCoroutine(MoveOnCurve(direction, duration, curve, speed));
    }
    
    public void MoveAlongACurveUsingRigidbody(Vector3 direction, float duration, float speed, AnimationCurve curve)
    {
        _monoBehaviour.StopCoroutine(MoveOnCurve(direction, duration, curve,speed));
        _monoBehaviour.StartCoroutine(MoveOnCurve(direction, duration, curve, speed));
    }

    private IEnumerator MoveOnCurve(Vector3 direction, float duration, AnimationCurve curve, float speed)
    {
        float expiredSeconds = 0;
        float progress = 0;
        var startPosition = _transform.position;
        var positionWithoutExternalChanges = startPosition;

        while (progress < 1)
        {
            expiredSeconds += Time.deltaTime;
            progress = expiredSeconds / duration;
            
            var forward = direction * (curve.Evaluate(progress) * speed);
            Vector3 target = startPosition + forward;
            _characterController.Move(target - positionWithoutExternalChanges);
            positionWithoutExternalChanges = target;
            yield return null;
        }
    }

    public void AddForce(Vector3 direction, ForceMode forceMode)
    {
        _rigidbody.AddForce(direction, forceMode);
    }

    public void Rotate(Vector3 direction) => 
        _transform.forward = GetRotationVector(direction);

    private Vector3 GetRotationVector(Vector3 direction)
    {
        Vector3 movementVector = Vector3.zero;
        if (direction.sqrMagnitude > Constants.Epsilon)
        {
            movementVector = _camera.transform.TransformDirection(direction);
            movementVector.y = 0;
            movementVector.Normalize();
        }

        return movementVector;
    }

    public void Rotate(Transform target)
    {
        Vector3 dir = target.transform.position - _transform.position;
        var newDir = new Vector3(dir.x, 0, dir.z);
        _transform.rotation = Quaternion.LookRotation(newDir);
    }
}