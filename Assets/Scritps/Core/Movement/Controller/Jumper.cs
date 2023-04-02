using Core.Movement.Data;
using Player.PlayerAnimation;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class Jumper : MonoBehaviour
    {
        private AnimatorController _animator;
        private JumpData _jumpData;
        private Rigidbody2D _rigidbody;
        private float _startTime;

        public bool IsJumping { get; private set; }

        public Jumper(Rigidbody2D rigidbody2D, JumpData jumpData, AnimatorController animator)
        {
            _rigidbody = rigidbody2D;
            _jumpData = jumpData;
            _animator = animator;
        }

        private void SwitchJump() => _animator.SwitchJump(IsJumping);

        public void Jump()
        {
            if (IsJumping)
                return;

            IsJumping = true;
            _startTime = Time.time;
            SwitchJump();
        }

        public void UpdateJump()
        {
            if (Time.time - _startTime > _jumpData.JumpTime)
                ResetJump();
        }

        private void ResetJump() {
            IsJumping = false;
            SwitchJump();
        }
    }
}