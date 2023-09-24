using System;
using System.Collections.Generic;
using Cam;
using Infrastructure.Services;
using PlayerLogic.Animation;
using UnityEngine;
public class LockOnCameraState : ICameraState, IDisposable
{
    public event Action<Transform> LockedTarget;
    public event Action UnlockedTarget;

    private IInputService _inputService;
    private PlayerCamera _camera;
    private Transform _selfTransform;
    private Transform _lockOnTransform;
    private PlayerStateAnimator _playerStateAnimator;
    private float _maximumLockOnDistance = 30;
    private float _lockedPivotPosition = 2.25f;
    private float _unlockedPivotPosition = 1.65f;
    
    private int _viewableAngel;
    
    private List<Lockable> _availableTargets = new List<Lockable>();
    private Transform _currentLockOnTarget;
    private Transform _cameraPivotTransform;
    private readonly Transform _playerTransform;
    private CameraFollow _cameraFollow;
    private Transform _leftLockTarget;
    private Transform _rightLockTarget;
    private LayerMask _environmentLayer;



    public LockOnCameraState(IInputService inputService,
        PlayerCamera camera,
        Transform selfTransform,
        Transform lockOnTransform,
        Transform cameraPivotTransform,
        Transform playerTransform,
        PlayerStateAnimator playerStateAnimator)
    {
        _inputService = inputService;
        _camera = camera;
        _selfTransform = selfTransform;
        _lockOnTransform = lockOnTransform;
        _cameraPivotTransform = cameraPivotTransform;
        _playerTransform = playerTransform;
        _playerStateAnimator = playerStateAnimator;
        
        _environmentLayer = LayerMask.NameToLayer("Environment");
        _cameraFollow = new CameraFollow(_selfTransform,
            playerTransform);
    }

    public void Enter()
    {
        Debug.Log("Lock in camera state");
        _inputService.LockOnButtonUp += LockOnButtonHandler;
        _inputService.LeftLockOnButtonUp += LockOnLeftTarget;
        _inputService.RightLockOnButtonUp += LockOnRightTarget;
        if (TryFindLockOnEnemy())
        {
            _currentLockOnTarget = GetAvailableTarget();
            LockedTarget?.Invoke(_currentLockOnTarget);
            _camera.SetTarget(_currentLockOnTarget);
            _playerStateAnimator.SetLockOnCamera(true);
            SetCameraHeight(_lockedPivotPosition);
        }
        else
            _camera.SwitchState<DefaultCameraState>();
    }

    public void FixedUpdate()
    {
        _cameraFollow.Follow();
        LockOn();
    }

    public void Exit()
    {
        SetCameraHeight(_unlockedPivotPosition);
        _availableTargets.Clear();
        _currentLockOnTarget = null;
        _playerStateAnimator.SetLockOnCamera(false);
        UnlockedTarget?.Invoke();
        _inputService.LockOnButtonUp -= LockOnButtonHandler;
        _inputService.LeftLockOnButtonUp -= LockOnLeftTarget;
        _inputService.RightLockOnButtonUp -= LockOnRightTarget;
    }

    public void Dispose()
    {
        _inputService.LockOnButtonUp -= LockOnButtonHandler;
        _inputService.LeftLockOnButtonUp -= LockOnLeftTarget;
        _inputService.RightLockOnButtonUp -= LockOnRightTarget;
    }

    private void SetCameraHeight(float pivotPosition)
    {
        Vector3 velocity = Vector3.zero;
        Vector3 newPosition = new Vector3(0, pivotPosition);
        _cameraPivotTransform.localPosition = Vector3
            .SmoothDamp(_cameraPivotTransform.localPosition
            ,newPosition, 
            ref velocity,
            Time.deltaTime);
    }
    private void LockOnLeftTarget()
    {
        if (TryFindLockOnLeftTarget(out Transform target))
        {
            _camera.SetTarget(target);
            LockedTarget?.Invoke(target);
        }
    }

    private void LockOnRightTarget()
    {
        if (TryFindLockOnRightTarget(out Transform target))
        {
            _camera.SetTarget(target);
            LockedTarget?.Invoke(target);
        }
    }

    private bool TryFindLockOnLeftTarget(out Transform target)
    {
        float shortestDistanceOnLeftTarget = Mathf.Infinity;
        Transform leftLockOnTarget = null;
        foreach (Lockable availableTarget in _availableTargets)
        {
            Vector3 relativePlayerPosition = _selfTransform.InverseTransformPoint(availableTarget.transform.position);
            
            float distanceFromLeftTarget = 1000f;

            
            distanceFromLeftTarget = GetDistanceFromLeftTarget(relativePlayerPosition, distanceFromLeftTarget, availableTarget);

            if (relativePlayerPosition.x < 0.00 && distanceFromLeftTarget < shortestDistanceOnLeftTarget)
            {
                shortestDistanceOnLeftTarget = distanceFromLeftTarget;
                leftLockOnTarget = availableTarget.LockOnTransform;
            }
        }

        if (leftLockOnTarget == null)
        {
            target = null;
            return false;
        }
        
        target = leftLockOnTarget;
        return true;

    }
    private bool TryFindLockOnRightTarget(out Transform target)
    {
        float shortestDistanceOnRightTarget = Mathf.Infinity;
        Transform rightLockTarget = null;
        foreach (Lockable availableTarget in _availableTargets)
        {
            Vector3 relativePlayerPosition = _selfTransform.InverseTransformPoint(availableTarget.transform.position);
            
            float distanceFromRightTarget = 1000f;

            
            distanceFromRightTarget = GetDistanceFromRightTarget(relativePlayerPosition, distanceFromRightTarget, availableTarget);

            if (relativePlayerPosition.x > 0.00 && distanceFromRightTarget < shortestDistanceOnRightTarget)
            {
                shortestDistanceOnRightTarget = distanceFromRightTarget;
                rightLockTarget = availableTarget.LockOnTransform;
            }
        }

        if (rightLockTarget == null)
        {
            target = null;
            return false;
        }
        
        target = rightLockTarget;
        return true;


    }
    private float GetDistanceFromLeftTarget(Vector3 relativePlayerPosition, float distanceFromLeftTarget,
        Lockable availableTarget)
    {
        if (relativePlayerPosition.x < 0.00)
            distanceFromLeftTarget =
                Vector3.Distance(_currentLockOnTarget.position, availableTarget.transform.position);

        return distanceFromLeftTarget;
    }
    private float GetDistanceFromRightTarget(Vector3 relativePlayerPosition, float distanceFromRightTarget,
        Lockable availableTarget)
    {
        if (relativePlayerPosition.x > 0.00)
            distanceFromRightTarget =
                Vector3.Distance(_currentLockOnTarget.position, availableTarget.transform.position);

        return distanceFromRightTarget;
    }

    private void SwitchTarget()
    {
        
    }
    private void LockOn()
    {
        _currentLockOnTarget = _camera.CurrentTarget;
        float velocity = 0; 
        
        Vector3 direction = _currentLockOnTarget.position - _selfTransform.position;
        direction.Normalize();
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _selfTransform.rotation = targetRotation;

        direction = _currentLockOnTarget.position - _cameraPivotTransform.position;
        direction.Normalize();

        targetRotation = Quaternion.LookRotation(direction);
        Vector3 eulerAngel = targetRotation.eulerAngles;
        eulerAngel.y = 0;
        _cameraPivotTransform.localEulerAngles = eulerAngel;
    }

    private bool TryFindLockOnEnemy()
    {
        _availableTargets?.Clear();
        Collider[] colliders = Physics.OverlapSphere(_lockOnTransform.position, 26);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Lockable lockable))
            {
                Vector3 colliderPosition = collider.transform.position;
                Vector3 lockTargetDirection = colliderPosition - _lockOnTransform.position;
                float distanceFromTarget = Vector3.Distance(_lockOnTransform.position, colliderPosition);
                RaycastHit hit;

                if (collider.transform.root != _lockOnTransform.transform.root
                    && _viewableAngel > -50 && _viewableAngel < 50
                    && distanceFromTarget <= _maximumLockOnDistance)
                {
                    Vector3 lockOnPlayerPosition = _playerTransform
                        .GetComponentInChildren<LockOnPlayer>()
                        .transform
                        .position;
                    
                    if (Physics.Linecast(lockOnPlayerPosition,
                        lockable.transform.position,
                        out hit))
                    {
                        Debug.DrawLine(lockOnPlayerPosition, lockable.transform.position);

                        if (hit.transform.gameObject.layer != _environmentLayer) 
                            _availableTargets.Add(lockable);
                    }                   
                    
                }
            }
        }

        if (_availableTargets.Count == 0)
        {
            _camera.SwitchState<DefaultCameraState>();
            return false;
        }
        return true;

    }

    private Transform GetAvailableTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Transform currentLockOnTarget = null;
        foreach (var availableTarget in _availableTargets)
        {
            float distanceFromTarget = Vector3.Distance(_lockOnTransform.position, availableTarget.transform.position);
            if (distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                currentLockOnTarget =  availableTarget.LockOnTransform;
            }
        }

        return currentLockOnTarget;
    }
    private void LockOnButtonHandler() => 
        _camera.SwitchState<DefaultCameraState>();
}