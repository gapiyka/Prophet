using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        [Header("Sprites")]
        [SerializeField] private Sprite[] _horizontalSprites;
        [SerializeField] private Sprite[] _verticalSprites;

        [Header("Movement")]
        [SerializeField] [Range(0f, 10f)] private float _speed;
        [SerializeField] private Vector2 _faceDirection;

        private Rigidbody2D _rigidbody;// check interpolate
        private SpriteRenderer _renderer;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>(); // mb use serializefield
            _renderer = GetComponent<SpriteRenderer>();
            SetFaceDirection(_faceDirection);
        }

        public void Move(Vector2 direction)
        {
            SetFaceDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity = direction * _speed; // what about <<v * Time.deltaTime>>
            _rigidbody.velocity = velocity;
        }

        private void SetFaceDirection(Vector2 direction)
        {
            int dirIndexX = (int)direction.x + 1;
            int dirIndexY = (int)direction.y + 1;
            Sprite sprite = (direction.x == 0f) ? 
                _verticalSprites[dirIndexY] :
                _horizontalSprites[dirIndexX];
            _renderer.sprite = sprite;
        }
    }
}

