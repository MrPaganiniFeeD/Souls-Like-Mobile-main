using System;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace.UI.Input
{
    public class MobileInputService : InputService, IDisposable
    {
        public override event Action LeftHandAttackButtonUp;
        public override event Action RightHandAttackButtonUp;
        public override event Action MainAttackButtonUp;
        public override event Action<Vector2> AxisChange;
        public override event Action<Vector2> RotationInputChange;
        public override event Action RollButtonUp;
        public override event Action LockOnButtonUp;
        public override event Action LeftLockOnButtonUp;
        public override event Action RightLockOnButtonUp;

        private Vector2 _currentAxis;
        private float _sensitivity = 2f;
        private RectTransform _transformRotationZone;
        private Vector2 _currentTouchPosition;
        private Vector2 _lastClickPosition;

        public override Vector2 Axis
        {
            get => _currentAxis;
            protected set
            {
                if (_currentAxis == value) return;

                AxisChange?.Invoke(value);
                _currentAxis = value;
            }
        }

        public MobileInputService(InputMap inputPlayerMap) : base(inputPlayerMap)
        {
            InputPlayerMap.Touchscreen.TouchPosition.performed += OnTouchPositionStarted;
            InputPlayerMap.Touchscreen.TouchPress.started += OnTouchPressStarted;
            InputPlayerMap.Touchscreen.TouchPress.canceled += OnTouchPressCanceled;
        }

        private void OnTouchPressStarted(InputAction.CallbackContext context)
        {
            Debug.Log("OnTouchPressStarted:");
            
            _currentTouchPosition = _lastClickPosition; 
            Debug.Log(_lastClickPosition);
            if (_transformRotationZone != null)
            {
                var isTouchInRect =
                    RectTransformUtility.RectangleContainsScreenPoint(_transformRotationZone, _currentTouchPosition);
                if (isTouchInRect)
                    InputPlayerMap.Touchscreen.TouchDelta.performed += OnTouchDeltaPerformed;
                else
                    _currentTouchPosition = Vector2.zero;
            }
        }

        private void OnTouchPressCanceled(InputAction.CallbackContext context)
        {
            Debug.Log("OnTouchPressCanceled");
            InputPlayerMap.Touchscreen.TouchDelta.performed -= OnTouchDeltaPerformed;
        }

        private void OnTouchPositionStarted(InputAction.CallbackContext context)
        {
            _lastClickPosition = context.ReadValue<Vector2>();
        }

        private void OnTouchDeltaPerformed(InputAction.CallbackContext context)
        {
            RotationInputChange?.Invoke(context.ReadValue<Vector2>());
        }


        public override void Update()
        {
            if(IsRolloverButtonUp())
                RollButtonUp?.Invoke();

            if (IsLeftHandAttackButtonUp())
            {
                LastClickedTimeLeftHandAttackButton = Time.time;
                LeftHandAttackButtonUp?.Invoke();
            }

            if (IsRightHandAttackButtonUp())
            {
                LastClickedTimeRightHandAttackButton = Time.time;
                RightHandAttackButtonUp?.Invoke();
            }

            if (IsMainAttackButtonUp())
            {
                MainAttackButtonUp?.Invoke();
            }
            
            if (IsLockOnClickButton())
                LockOnButtonUp?.Invoke();
            if (IsLeftLockOnClickButton())
                LeftLockOnButtonUp?.Invoke();
            if (IsRightLockOnClickButton())
                RightLockOnButtonUp?.Invoke();


            Axis = GetSimpleInputAxis();

            
        }

        public override void SetRotationZone(RectTransform transformRotationZone)
        {
            _transformRotationZone = transformRotationZone;
        }
        private bool IsLockOnClickButton() =>
            SimpleInput.GetButtonUp(LockOn);

        private bool IsLeftLockOnClickButton() =>
            SimpleInput.GetButtonUp(LeftLockOn);

        private bool IsRightLockOnClickButton() =>
            SimpleInput.GetButtonUp(RightLockOn);


        private static Vector2 GetSimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

        public void Dispose()
        {
            InputPlayerMap.Touchscreen.TouchPress.started -= OnTouchPressStarted;
            InputPlayerMap.Touchscreen.TouchPress.canceled -= OnTouchPressCanceled;
            InputPlayerMap.Touchscreen.TouchPosition.performed -= OnTouchPositionStarted;
        }
    }
}