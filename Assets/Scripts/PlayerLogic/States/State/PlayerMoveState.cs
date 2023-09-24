using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.Inventory;
using PlayerLogic.Animation;
using PlayerLogic.States.Transition;
using UnityEngine;

namespace PlayerLogic.States.State
{
    public class PlayerMoveState : PlayerState, IMoveState, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly Camera _camera;
        private readonly Transform _transform;
        private readonly PlayerStateAnimator _animator;
        private readonly IInventoryService _inventoryService;
        private readonly AudioSource _audioSource;
        private readonly Rigidbody _rigidbody;

        private float _speed = 5;
        private float _rotateSpeed = 10;
        private Vector3 _moveDirection;
        private Vector3 _targetPosition;
        private Vector3 _normalVector;
        private PlayerCamera _playerCamera;

        private bool _isLockOn = false;
        private CharacterController _characterController;

        public PlayerMoveState(List<ITransition> transition,
            IInputService inputService,
            Camera camera,
            Transform transform,
            CharacterController characterController,
            PlayerStateAnimator animator,
            IInventoryService inventoryService,
            AudioSource audioSource) 
            : base(transition)
        {
            _inputService = inputService;
            _camera = camera;
            _transform = transform;
            _characterController = characterController;
            _animator = animator;
            _inventoryService = inventoryService;
            _audioSource = audioSource;

            
            _playerCamera = _camera.GetComponentInParent<PlayerCamera>();
            _playerCamera.LockedTarget += OnLockedTarget;
            _playerCamera.UnlockedTarget += OnUnlockedTarget;     
        }


        public void Enter(IMoveStatePayloaded payloaded)
        {
            base.Enter();
            _speed = payloaded.Speed;
        }

        public override void Enter()
        {
            base.Enter();
            _animator.StartDefaultMoveAnimation();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Move(_inputService.Axis);
            if (_isLockOn)
            {
                RotateToTarget();
                _animator.UpdateVelocity(_inputService.Axis.x, _inputService.Axis.y);
            }
            else
            {
                RotateToMove();
                _animator.UpdateVelocity(0, 1);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _animator.StopDefaultMoveAnimation();
        }
 
        public void Dispose()
        {
            _playerCamera.LockedTarget -= OnLockedTarget;
            _playerCamera.UnlockedTarget -= OnUnlockedTarget;
        }

        private void OnLockedTarget(Transform target) => 
            _isLockOn = true;

        private void OnUnlockedTarget() => 
            _isLockOn = false;

        private void Move(Vector2 direction)
        {
            _moveDirection = _camera.transform.forward * direction.y;
            _moveDirection += _camera.transform.right * direction.x;
            _moveDirection.Normalize();
            _moveDirection.y = 0;
            _moveDirection *= _speed* Time.fixedDeltaTime;

            _characterController.Move(_moveDirection);
        }


        private void RotateToMove() => 
            _transform.rotation = Quaternion.LookRotation(_moveDirection);

        private void Rotate(Vector2 direction)
        {
            Vector3 targetDir = Vector3.zero;

            targetDir = _camera.transform.forward * direction.y;
            targetDir += _camera.transform.right * direction.x;
            
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = _transform.forward;
            
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion _targetRotation = Quaternion.Slerp(_transform.rotation, tr, _rotateSpeed * Time.deltaTime);
            _transform.rotation = _targetRotation;
        }

        private void RotateToTarget()
        {
            Vector3 rotationDirection = _moveDirection;
            rotationDirection = _playerCamera.CurrentTarget.position - _transform.position;
            rotationDirection.y = 0;
            rotationDirection.Normalize();
            Quaternion tr = Quaternion.LookRotation(rotationDirection); 
            Quaternion targetRotation = Quaternion.Slerp(_transform.rotation, tr, _rotateSpeed * Time.deltaTime);
            
            _transform.rotation = targetRotation;
        }
    }
}