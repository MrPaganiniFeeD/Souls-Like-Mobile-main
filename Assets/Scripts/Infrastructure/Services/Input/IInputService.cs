using System;
using UnityEngine;

namespace Infrastructure.Services
{
    public interface IInputService : IUpdateableService
    {
        event Action LeftHandAttackButtonUp;
        event Action RightHandAttackButtonUp;
        event Action MainAttackButtonUp;
        event Action<Vector2> AxisChange;
        event Action<Vector2> RotationInputChange;
        event Action RollButtonUp;
        event Action LockOnButtonUp;
        event Action LeftLockOnButtonUp;
        event Action RightLockOnButtonUp;
        
        public float MouseY { get; }

        public float MouseX { get; }


        public float LastClickedTimeLeftHandAttackButton { get; }
        public float LastClickedTimeRightHandAttackButton { get; }

        Vector2 Axis { get; }

        bool IsLeftHandAttackButtonUp();

        public void SetRotationZone(RectTransform transformRotationZone);
    }
}