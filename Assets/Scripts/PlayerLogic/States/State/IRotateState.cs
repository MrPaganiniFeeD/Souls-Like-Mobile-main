using UnityEngine;

namespace PlayerLogic.States.State
{
    public interface IRotateState
    {
        void Rotate(Vector3 direction);
    }
}