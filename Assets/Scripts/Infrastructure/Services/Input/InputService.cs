using System;
using UnityEngine;

namespace Infrastructure.Services
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string LeftHandAttack = "LeftHandAttack";
        protected const string RightHandAttack = "RightHandAttack";
        protected const string Rollover = "Rollover";
        protected const string LockOn = "LockOn";
        protected const string LeftLockOn = "LeftLockOn";
        protected const string RightLockOn = "RightLockOn";
        protected const string MainAttack = "MainAttack";

        public abstract event Action LeftHandAttackButtonUp;
        public abstract event Action RightHandAttackButtonUp;
        public abstract event Action MainAttackButtonUp;
        public abstract event Action<Vector2> ChangeAxis;
        public abstract event Action RollButtonUp;
        public abstract event Action LockOnButtonUp;
        public abstract event Action LeftLockOnButtonUp;
        public abstract event Action RightLockOnButtonUp;

        public float MouseY { get; protected set; }
        public float MouseX { get; protected set; }

        public float LastClickedTimeLeftHandAttackButton { get; protected set; }
        public float LastClickedTimeRightHandAttackButton { get; protected set; }
        public abstract Vector2 Axis { get; protected set; }
        
        
        public bool IsLeftHandAttackButtonUp() =>
            SimpleInput.GetButtonUp(LeftHandAttack);

        public abstract void SetAimArea(AimArea aimArea);

        protected bool IsRightHandAttackButtonUp() =>
            SimpleInput.GetButtonUp(RightHandAttack);

        protected bool IsRolloverButtonUp() => 
            SimpleInput.GetButtonUp(Rollover);

        protected bool IsLockOnButtonUp() =>
            SimpleInput.GetButtonUp(LockOn);

        protected bool IsMainAttackButtonUp() =>
            SimpleInput.GetButtonUp(MainAttack);
        public abstract void Update();
    }
}