using System;
using UnityEngine;

namespace Hero
{
    public class GravitySystem : MonoBehaviour
    {
        [SerializeField] private float _groundedYVlocity = -5;
        [SerializeField] private Vector3 _yVelocity;
        
        private CharacterController _characterController;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _yVelocity.y = _groundedYVlocity;
            _characterController.Move(_yVelocity * Time.deltaTime);
        }
    }
}