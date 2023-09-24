using System;
using System.Collections.Generic;
using Cam;
using Infrastructure.Services;
using PlayerLogic.Animation;
using PlayerLogic.States;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCamera : MonoBehaviour
    {
        public event Action<Transform> LockedTarget;
        public event Action UnlockedTarget;

        public Transform CurrentTarget { get; private set; }
        public bool IsLockedTarget { get; private set; }

        [FormerlySerializedAs("_selfTransform")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _pivotTransform;
        
        private Transform _target;
        private Transform _lockOnTransform;
        private Dictionary<Type, ICameraState> _states;
        private ICameraState _currentState;
        private LockOnCameraState _lockOnCameraState;

        public void Construct(Player player, IInputService inputService)
        {
            _target = player.transform;
            _lockOnTransform = player.GetComponentInChildren<LockOnPlayer>().transform; 
            _states = new Dictionary<Type, ICameraState>
            {
                [typeof(DefaultCameraState)] = new DefaultCameraState(inputService, 
                    gameObject.GetComponent<PlayerCamera>(),
                    _cameraTransform,
                    _target,
                    _pivotTransform,
                    transform,
                    player.GetComponent<PlayerStateAnimator>())
            };
            _lockOnCameraState = new LockOnCameraState(inputService,
                gameObject.GetComponent<PlayerCamera>(),
                transform,
                _lockOnTransform,
                _pivotTransform,
                _target,
                player.GetComponent<PlayerStateAnimator>());
            _states.Add(typeof(LockOnCameraState), _lockOnCameraState);
            _lockOnCameraState.LockedTarget += OnLockedTarget;
            _lockOnCameraState.UnlockedTarget += OnUnlockedTarget;
            _currentState = _states[typeof(DefaultCameraState)];
            _currentState.Enter();
        }

        public void FixedUpdate() => 
            _currentState?.FixedUpdate();

        public void SetTarget(Transform target)
        {
            if (target != null)
                CurrentTarget = target;
        }

        public void SwitchState<T>() where T : class, ICameraState
        {
            ICameraState state = ChangeState<T>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, ICameraState
        {
            TState newState = GetState<TState>();
            _currentState?.Exit();
            _currentState = newState;
            return newState;
        }

        private TState GetState<TState>() where TState : class, ICameraState => 
            _states[typeof(TState)] as TState;

        private void OnUnlockedTarget()
        {
            IsLockedTarget = false;
            UnlockedTarget?.Invoke();
        }

        private void OnLockedTarget(Transform target)
        {
            IsLockedTarget = true;
            LockedTarget?.Invoke(target);
        }

        public void OnDestroy()
        {
            if (_lockOnCameraState != null)
            {
                _lockOnCameraState.LockedTarget -= OnLockedTarget;
                _lockOnCameraState.UnlockedTarget -= OnUnlockedTarget;
            }
        }
    }