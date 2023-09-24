using UnityEngine;

namespace Animation
{
    public class MovementDuringAnimation : StateMachineBehaviour
    {
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _transform;

        private float _speed;
        private float _currentTime;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _speed = _animationCurve.Evaluate(_currentTime);
            _currentTime += Time.deltaTime;
            var offset = _speed * _transform.forward * Time.deltaTime;
            _rigidbody.MovePosition(_rigidbody.position + offset);  
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _currentTime = 0;
        }
    }
}
