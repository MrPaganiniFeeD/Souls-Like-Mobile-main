using System;
using System.Net.Http.Headers;
using Cam;
using Infrastructure.Services;
using PlayerLogic.Animation;
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


        public void Enter() => 
            _playerStateAnimator.SetDefaultCamera();

        public void FixedUpdate()
        {
            _cameraFollow.Follow();
            _cameraRotation.Rotation();
            _cameraCollisions.UpdateCollisions();
        }

        public void Exit() { }


        private void LockOnButtonHandler() => 
            _camera.SwitchState<LockOnCameraState>();

        public void Dispose() => 
            _inputService.LockOnButtonUp -= LockOnButtonHandler;
    }