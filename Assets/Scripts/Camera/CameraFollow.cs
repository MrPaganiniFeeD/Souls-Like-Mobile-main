using UnityEngine;

namespace Cam
{
    public class CameraFollow
    {
        private Transform _selfTransform;
        private Transform _playerTransform;
        private float _followSpeed = 0.1f;
        private Vector3 _cameraFollowVelocity = Vector3.zero;

        
        public CameraFollow(Transform selfTransform,
            Transform playerTransform)
        {
            _selfTransform = selfTransform;
            _playerTransform = playerTransform;
        }

        public void Follow()
        {
            Vector3 targetPosition = Vector3
                .SmoothDamp(_selfTransform.position, 
                    _playerTransform.position,
                    ref _cameraFollowVelocity, 
                    Time.fixedDeltaTime / _followSpeed);
            _selfTransform.position = targetPosition;
        }
    }
}