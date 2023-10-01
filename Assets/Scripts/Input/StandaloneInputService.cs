using System;
using Infrastructure.Services;
using UnityEngine;

    public class StandaloneInputService : InputService
    {
        public override event Action LeftHandAttackButtonUp;
        public override event Action RightHandAttackButtonUp;
        public override event Action MainAttackButtonUp;
        public override event Action<Vector2> ChangeAxis;
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

                ChangeAxis?.Invoke(value);
                _currentAxis = value;
            }
        }

        public override void SetAimArea(AimArea aimArea) { }


        public override void Update()
        {
            if(IsRolloverButtonUp() || IsSpaceButtonUp())
                RollButtonUp?.Invoke();
            if ((IsLeftHandAttackButtonUp() || IsLeftClickMouseButton()) && IsMainAttackButtonUp() == false)
            {
                LastClickedTimeLeftHandAttackButton = _lastClickLeftButtonTime;
                LeftHandAttackButtonUp?.Invoke();
                _lastClickLeftButtonTime = Time.time;
            }

            if ((IsRightHandAttackButtonUp() || IsRightClickMouseButton()) && IsMainAttackButtonUp() == false)
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
            MouseX = SimpleInput.GetAxis("MouseX");
            MouseY = SimpleInput.GetAxis("MouseY");

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

        private bool IsRightLockOnClickButton() =>
            SimpleInput.GetButtonUp(RightLockOn);
        
        
        private static Vector2 GetSimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

        private static Vector2 GetUnityAxis() =>
            new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    }
