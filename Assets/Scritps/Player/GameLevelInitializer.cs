using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private UIInputHandler _uIInputHandler;

        private ExternalDeviceInputHandler _externalDeviceInputHandler;
        private PlayerBrain _playerBrain;

        private bool _onPause;

        private void Awake()
        {
            _externalDeviceInputHandler = new ExternalDeviceInputHandler();
            _playerBrain = new PlayerBrain(_playerEntity, new List<IEntityInputSource>
            {
                _uIInputHandler,
                _externalDeviceInputHandler
            });
        }

        private void Update()
        {
            if (_onPause)
                return;

            _externalDeviceInputHandler.OnUpdate();
        }
        private void FixedUpdate()
        {
            if (_onPause)
                return;

            _playerBrain.OnFixedUpdate();
        }
    }
}
