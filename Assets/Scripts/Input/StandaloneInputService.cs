using System;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.InputSystem;

public class StandaloneInputService : InputService, IDisposable
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
        private float _lastClickLeftButtonTime;
        private float _lastClickRightButtonTime;

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

        public StandaloneInputService(InputMap inputMap) : base(inputMap)
        {
            InputPlayerMap.Mouse.MouseDelta.performed += OnMouseDeltaPerformed;
        }

        public override void SetRotationZone(RectTransform transformRotationZone)
        {
            
        }


        public override void Update()
        {
            if(IsRolloverButtonUp() || IsSpaceButtonUp())
                RollButtonUp?.Invoke();
            if ((IsLeftHandAttackButtonUp()) && IsMainAttackButtonUp() == false)
            {
                LastClickedTimeLeftHandAttackButton = _lastClickLeftButtonTime;
                LeftHandAttackButtonUp?.Invoke();
                _lastClickLeftButtonTime = Time.time;
            }

            if ((IsRightHandAttackButtonUp()) && IsMainAttackButtonUp() == false)
            {
                LastClickedTimeRightHandAttackButton = _lastClickRightButtonTime;
                RightHandAttackButtonUp?.Invoke();
                _lastClickRightButtonTime = Time.time;
            }

            if (IsMainAttackButtonUp())
                MainAttackButtonUp?.Invoke();
            if (IsLockOnClickButton())
                LockOnButtonUp?.Invoke();
            if (IsLeftLockOnClickButton())
                LeftLockOnButtonUp?.Invoke();
            if (IsRightLockOnClickButton())
                RightLockOnButtonUp?.Invoke();    

            
            Axis = GetSimpleInputAxis();
            RotationInputChange?.Invoke(new Vector2(Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y")));
        }

        private bool IsSpaceButtonUp() => 
            SimpleInput.GetKey(KeyCode.Space);

        private bool IsRightClickMouseButton() => 
            SimpleInput.GetKey(KeyCode.Mouse1);

        private bool IsLeftClickMouseButton() => 
            SimpleInput.GetMouseButtonUp(0);

        private bool IsLockOnClickButton() =>
            SimpleInput.GetButtonUp(LockOn);

        private bool IsLeftLockOnClickButton() =>
            SimpleInput.GetButtonUp(LeftLockOn);

        private void OnMouseDeltaPerformed(InputAction.CallbackContext context) => 
            RotationInputChange?.Invoke(context.ReadValue<Vector2>());

        private bool IsRightLockOnClickButton() =>
            SimpleInput.GetButtonUp(RightLockOn);


        private static Vector2 GetSimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

        private static Vector2 GetUnityAxis() =>
            new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

        public void Dispose() => 
            InputPlayerMap.Mouse.MouseDelta.performed -= OnMouseDeltaPerformed;
    }
