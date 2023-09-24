using Infrastructure.Services;
using UnityEngine;

namespace Cam
{
    public class CameraRotate
    {
        private IInputService _inputService;
        private float _lookAngel;
        private float _pivotAngel;
        private float _minimumPivot = -35;
        private float _maximumPivot = 35;
        private float _lookSpeed = 0.1f;
        private float _pivotSpeed = 0.07f;
        private Transform _selfTransform;
        private Transform _cameraPivotTransform;

        public CameraRotate(Transform selfTransform,
            Transform cameraPivotTransform,
            IInputService inputService)
        {
            _selfTransform = selfTransform;
            _cameraPivotTransform = cameraPivotTransform;
            _inputService = inputService;
        }

        public void Rotation()
        {
            _lookAngel += (_inputService.MouseX * _lookSpeed) / Time.fixedDeltaTime;
            _pivotAngel -= (_inputService.MouseY * _pivotSpeed) / Time.fixedDeltaTime;
            _pivotAngel = Mathf.Clamp(_pivotAngel, _minimumPivot, _maximumPivot);
            
            Vector3 rotation = Vector3.zero;
            rotation.y = _lookAngel;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            _selfTransform.rotation = targetRotation;
            
            rotation = Vector3.zero;
            rotation.x = _pivotAngel;

            targetRotation = Quaternion.Euler(rotation);
            _cameraPivotTransform.localRotation = targetRotation;
        }
    }
}