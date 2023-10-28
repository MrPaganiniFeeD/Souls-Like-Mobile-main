using Infrastructure.Services;
using UnityEditor;
using UnityEngine;

namespace Cam
{
    public class CameraRotate
    {
        private float _lookAngel;
        private float _pivotAngel;
        private float _minimumPivot = -35;
        private float _maximumPivot = 35;
        private float _lookSpeed = 0.05f;
        private float _pivotSpeed = 0.035f;
        private Transform _selfTransform;
        private Transform _cameraPivotTransform;

        public CameraRotate(Transform selfTransform,
            Transform cameraPivotTransform,
            IInputService inputService)
        {
            _selfTransform = selfTransform;
            _cameraPivotTransform = cameraPivotTransform;
        }

        public void Rotation(Vector2 rotateDelta)
        {
            _lookAngel += (rotateDelta.x * _lookSpeed) / Time.deltaTime;
            _pivotAngel -= (rotateDelta.y * _pivotSpeed) / Time.deltaTime;
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

        public void Rotation(Quaternion rotation)
        {
            _selfTransform.rotation = rotation;
            
            _lookAngel = rotation.x;
            _pivotAngel = rotation.y;
        }
    }
}