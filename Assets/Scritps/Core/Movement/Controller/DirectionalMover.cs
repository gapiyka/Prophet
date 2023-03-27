using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class DirectionalMover
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly DirectionalMovementData _directionalMovementData;

        public Vector2 Direction { get; private set; }

        public DirectionalMover(Rigidbody2D rigidbody, DirectionalMovementData directionalMovementData)
        {
            _rigidbody = rigidbody;
            _directionalMovementData = directionalMovementData;
        }


        public void Move(Vector2 direction)
        {
            Direction = direction;
            Vector2 velocity = _rigidbody.velocity;
            velocity = direction * _directionalMovementData.Speed;
            _rigidbody.velocity = velocity;
        }
    }
}