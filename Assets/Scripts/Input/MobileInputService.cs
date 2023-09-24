using System;
using Infrastructure.Services;
using UnityEngine;

namespace DefaultNamespace.UI.Input
{
    public class MobileInputService : InputService
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
        private float _sensitivity = 0.01f;

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

            MouseX = UnityEngine.Input.GetTouch(0).deltaPosition.x * _sensitivity;
            MouseY = UnityEngine.Input.GetTouch(0).deltaPosition.y * _sensitivity;
        }
        
        private bool IsLockOnClickButton() =>
            SimpleInput.GetButtonUp(LockOn);

        private bool IsLeftLockOnClickButton() =>
            SimpleInput.GetButtonUp(LeftLockOn);

        private bool IsRightLockOnClickButton() =>
            SimpleInput.GetButtonUp(RightLockOn);
        

        private static Vector2 GetSimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}