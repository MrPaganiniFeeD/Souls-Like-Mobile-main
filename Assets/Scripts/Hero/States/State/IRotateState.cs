using UnityEngine;

namespace Hero.States.State
{
    public interface IRotateState
    {
        void Rotate(Vector3 direction);
    }
}