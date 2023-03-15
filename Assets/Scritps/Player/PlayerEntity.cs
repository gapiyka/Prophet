using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] [Range(0f, 10f)] private float _speed;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _animationDelay;

        private Rigidbody2D _rigidbody;
        private bool _isMoving;
        private bool _isJumping;

        public void Move(Vector2 direction)
        {
            if (_isJumping)
                return;

            SetFaceDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity = direction * _speed; // what about <<v * Time.deltaTime>>
            _rigidbody.velocity = velocity;
        }

        public void Jump()
        {
            if (!_isJumping)
            {
                SwitchJump(true);
                StartCoroutine(Fall());
            }
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>(); // mb use serializefield
            SetFaceDirection(new Vector2(0f, 0f));
            _isJumping = false;
        }

        private IEnumerator Fall()
        {
            yield return new WaitForSeconds(_animationDelay);
            SwitchJump(false);
        }

        private void SwitchJump(bool state)
        {
            _isJumping = state;
            _animator.SetBool("IsJumping", state);
        }

        private void SetFaceDirection(Vector2 direction)
        {
            _isMoving = (direction.magnitude > 0 && !_isJumping) ? true : false;
            _animator.SetFloat("Horizontal", direction.x);
            _animator.SetFloat("Vertical", direction.y);
            _animator.SetBool("IsMoving", _isMoving);
        }
    }
}

