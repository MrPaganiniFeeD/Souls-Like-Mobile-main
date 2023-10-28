using UnityEngine;

namespace Cam
{
    public interface ICameraState
    {
        void Enter();
        void Enter(Quaternion rotation);
        void Update();
        void Exit();
    }
}