using System;
using UnityEngine;

namespace Observer
{
    public class ObservableTransform : MonoBehaviour, IObservableTransform
    {
        public event Action<Transform> OnChangePosition;

        private Transform _selfTransform;
        private Vector3 _lastPosition;

        private void Awake()
        {
            _selfTransform = GetComponent<Transform>();
            _lastPosition = _selfTransform.position;
        }
        private void Update()
        {
            if(_selfTransform.position != _lastPosition)
                OnChangePosition?.Invoke(_selfTransform);
        
            _lastPosition = _selfTransform.position;
        }
        public Transform GetTransform()
        {
            return _selfTransform;
        }
    }
}
