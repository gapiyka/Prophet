using Player;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerEntity _player;

    private Vector2 _direction;
    private bool _isJumping;

    private void Start() => _direction = new Vector2(0f, 0f);

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            _player.Jump();
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() =>_player.Move(_direction);
}
