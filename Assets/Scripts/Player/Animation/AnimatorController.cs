using UnityEngine;

namespace Player.PlayerAnimation
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void UpdateAnimations(Vector2 direction)
        {
            bool isMoving = direction.magnitude > 0;
            _animator.SetFloat("Horizontal", direction.x);
            _animator.SetFloat("Vertical", direction.y);
            _animator.SetBool("IsMoving", isMoving);
        }

        public void SwitchJump(bool state)
        {
            _animator.SetBool("IsJumping", state);
        }
    }
}