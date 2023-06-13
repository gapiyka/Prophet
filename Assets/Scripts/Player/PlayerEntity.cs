using Core.Movement.Controller;
using Core.Movement.Data;
using Player.PlayerAnimation;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private DirectionalMovementData _directionalMovementData;
        [SerializeField] private JumpData _jumpData;

        private Rigidbody2D _rigidbody;
        private DirectionalMover _directionalMover;
        private Jumper _jumper;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _directionalMover = new DirectionalMover(_rigidbody, 
                _directionalMovementData);
            _jumper = new Jumper(_rigidbody, _jumpData, _animator);
        }

        private void Update()
        {
            if (!_jumper.IsJumping)
                _animator.UpdateAnimations(_directionalMover.Direction);
            else
                _jumper.UpdateJump();
        }

        public void Move(Vector2 direction) => _directionalMover.Move(direction);

        public void Jump() => _jumper.Jump();
    }
}