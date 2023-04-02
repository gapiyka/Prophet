using System;
using System.Collections.Generic;
using Core.Services.Updater;
using InputReader;
using Player;
using UnityEngine;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private UIInputHandler _gameUIInputView;

        private ExternalDevicesInputReader _externalDevicesInput;
        private PlayerSystem _playerSystem;
        private ProjectUpdater _projectUpdater;

        private List<IDisposable> _disposables;
        private List<IEntityInputSource> _inputSources;

        private void Awake()
        {
            _disposables = new List<IDisposable>();
            if (ProjectUpdater.Instance == null)
                _projectUpdater = new GameObject().AddComponent<ProjectUpdater>();
            else 
                _projectUpdater = ProjectUpdater.Instance as ProjectUpdater;
            
            _externalDevicesInput = new ExternalDevicesInputReader();
            _disposables.Add(_externalDevicesInput);
            _inputSources = new List<IEntityInputSource>
            {
                _gameUIInputView,
                _externalDevicesInput
            };
            _playerSystem = new PlayerSystem(_playerEntity, _inputSources);
        }

        private void Update()
        {
            foreach (var input in _inputSources)
                if (input.Pause)
                {
                    input.ResetOneTimeActions();
                    _projectUpdater.IsPaused = !_projectUpdater.IsPaused;
                    return;
                }
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}