using System;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.UI.Input
{
    public class MobileInputService : InputService
    {
        private AimArea _aimArea;
        public override event Action LeftHandAttackButtonUp;
        public override event Action RightHandAttackButtonUp;
        public override event Action MainAttackButtonUp;
        public override event Action<Vector2> ChangeAxis;
        public override event Action RollButtonUp;
        public override event Action LockOnButtonUp;
        public override event Action LeftLockOnButtonUp;
        public override event Action RightLockOnButtonUp;

        private Vector2 _currentAxis;
        private float _sensitivity = 2f;

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

            if (_aimArea != null)
            {
                if (_aimArea.IsClicked)
                {
                    ChangePointerPosition(UnityEngine.Input.GetTouch(0).deltaPosition);
                }
                else
                {
                    ChangePointerPosition(Vector2.zero);    
                }
            }

        }

        public override void SetAimArea(AimArea aimArea)
        {
            _aimArea = aimArea;
        }

        private void ChangePointerPosition(Vector2 position)
        {
            MouseX = position.x * _sensitivity * Time.deltaTime;
            MouseY = position.y * _sensitivity * Time.deltaTime;
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