using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class PlayerBrain
    {
        private readonly PlayerEntity _playerEntity;
        private readonly List<IEntityInputSource> _inputSources;
        private Vector2 _direction;

        public PlayerBrain(PlayerEntity player, List<IEntityInputSource> inputSources)
        {
            _playerEntity = player;
            _inputSources = inputSources;
        }

        public void OnFixedUpdate()
        {
            _playerEntity.Move(GetDirection());
            if(IsJump)
                _playerEntity.Jump();
            foreach (var input in _inputSources)
                input.ResetOneTimeActions();
        }

        private Vector2 GetDirection()
        {
            foreach(var input in _inputSources)
            {
                _direction.x = input.HorizontalDirection;
                _direction.y = input.VerticalDirection;

                if (input.HorizontalDirection == 0f &&
                    input.VerticalDirection == 0)
                    continue;

                return _direction;
            }
            return _direction;
        }

        private bool IsJump => _inputSources.Any(source => source.Jump);
    }
}
