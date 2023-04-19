using System;
using System.Collections.Generic;
using Core.Services.Updater;
using InputReader;
using Player;
using UnityEngine;
using Items;
using Items.Interface.Rarity;
using Items.Storage;
using Items.Data;
using System.Linq;
using UI;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private UIInputHandler _gameUIInputView;
        [SerializeField] private ItemRarityStorage _rarityDescriptorsStorage;
        [SerializeField] private LayerMask _whatIsPlayer;
        [SerializeField] private ItemsStorage _itemsStorage;

        private ExternalDevicesInputReader _externalDevicesInput;
        private PlayerSystem _playerSystem;
        private ProjectUpdater _projectUpdater;
        private ItemsSystem _itemsSystem;
        private DropGenerator _dropGenerator;
        private UIContext _uiContext;

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

            ItemsFactory itemsFactory = new ItemsFactory();
            List<IItemRarityColor> rarityColors = _rarityDescriptorsStorage.RarityDescriptors.Cast<IItemRarityColor>().ToList();
            _itemsSystem = new ItemsSystem(rarityColors, _whatIsPlayer, itemsFactory, _playerSystem.Inventory);
            List<ItemDescriptor> descriptors = _itemsStorage.ItemScriptables.Select(scriptable => scriptable.ItemDescriptor).ToList();
            _dropGenerator = new DropGenerator(descriptors, _playerEntity, _itemsSystem);
            UIContext.Data data = new UIContext.Data(_playerSystem.Inventory, _rarityDescriptorsStorage.RarityDescriptors);
            _uiContext = new UIContext(_inputSources.Cast<IWindowsInputSource>().ToList(), data);
        }

        private void Update()
        {
            foreach (var input in _inputSources)
            {
                if (input.Pause)
                {
                    input.ResetOneTimeActions();
                    if (_projectUpdater.IsPaused)
                        _uiContext.CloseCurrentScreen();
                    _projectUpdater.IsPaused = !_projectUpdater.IsPaused;
                    return;
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}