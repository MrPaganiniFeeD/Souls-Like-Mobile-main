using UnityEngine;

namespace Cam
{
    public class CameraCollisions
    {
        private readonly Transform _cameraTransform;
        private readonly Transform _cameraPivotTransform;
        private float _targetPosition;
        private float _defaultPosition;
        private float _cameraSphereRadius = 0.1f;
        private float _cameraCollisionOffset = 0.01f;
        private float _minimumCollisionOffset = 0.9f;
        private LayerMask _ignoreLayers;
        private Vector3 _cameraTransformPosition;

        public CameraCollisions(Transform cameraTransform, 
            Transform cameraPivotTransform)
        {
            _cameraTransform = cameraTransform;
            _cameraPivotTransform = cameraPivotTransform;
            _defaultPosition = cameraTransform.localPosition.z;
            _ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }
        
        public void UpdateCollisions()
        {
            _targetPosition = _defaultPosition;
            RaycastHit hit;
            Vector3 direction = _cameraTransform.position - _cameraPivotTransform.position;
            direction.Normalize();

            if (Physics.SphereCast(_cameraPivotTransform.position,
                _cameraSphereRadius,
                direction,
                out hit,
                Mathf.Abs(_targetPosition),
                _ignoreLayers))
            {
                float distance = Vector3.Distance(_cameraPivotTransform.position, hit.point);
                _targetPosition = -(distance - _cameraCollisionOffset);
            }

            if (Mathf.Abs(_targetPosition) < _minimumCollisionOffset) 
                _targetPosition = -_minimumCollisionOffset;

            _cameraTransformPosition.z =
                Mathf.Lerp(_cameraTransform.localPosition.z, _targetPosition, Time.fixedDeltaTime/ 0.1f);
            _cameraTransform.localPosition = _cameraTransformPosition;

        }
    }
}