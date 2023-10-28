using System;
using Cam;
using Infrastructure.Services;
using UnityEngine;

public class DefaultCameraState : ICameraState, IDisposable
{
        private float _smooth = 1;
        
        private readonly IInputService _inputService;
        private readonly PlayerCamera _camera;
        private readonly Transform _cameraTransform;
        private readonly Transform _cameraPivotTransform;
        private readonly PlayerStateAnimator _playerStateAnimator;
        private CameraRotate _cameraRotation;
        private CameraCollisions _cameraCollisions;
        private CameraFollow _cameraFollow;

        private float _followSpeed = 0.1f;


        public DefaultCameraState(IInputService inputService,
            PlayerCamera camera,
            Transform cameraTransform,
            Transform playerTransform,
            Transform cameraPivotTransform,
            Transform selfTransform,
            PlayerStateAnimator playerStateAnimator)
        {
            _inputService = inputService;
            _camera = camera;
            _cameraTransform = cameraTransform;
            _playerStateAnimator = playerStateAnimator;
            _cameraPivotTransform = cameraPivotTransform;

            _inputService.LockOnButtonUp += LockOnButtonHandler;
            
            _cameraRotation = new CameraRotate(selfTransform, 
                _cameraPivotTransform,
                _inputService);
                _cameraCollisions = new CameraCollisions(_cameraTransform,
                    _cameraPivotTransform);
            _cameraFollow = new CameraFollow(selfTransform,
                playerTransform);
        }


        public void Enter()
        {
            _playerStateAnimator.SetDefaultCamera();
            _inputService.RotationInputChange += Rotate;
        }

        public void Enter(Quaternion rotation)
        {
            _cameraRotation.Rotation(rotation);
            _inputService.RotationInputChange += Rotate;
        }

        public void Update()
        {
            _cameraFollow.Follow();
            _cameraCollisions.UpdateCollisions();
        }

        public void Exit() => 
            _inputService.RotationInputChange -= Rotate;

        public void Dispose()
        {
            _inputService.RotationInputChange -= Rotate;
            _inputService.LockOnButtonUp -= LockOnButtonHandler;
        }

        private void Rotate(Vector2 context) => 
            _cameraRotation.Rotation(context);


        private void LockOnButtonHandler() => 
            _camera.SwitchState<LockOnCameraState>();
}