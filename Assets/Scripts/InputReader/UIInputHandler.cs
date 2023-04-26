using System;
using UnityEngine;
using UnityEngine.UI;

namespace InputReader
{
    public class UIInputHandler : MonoBehaviour, IEntityInputSource, IWindowsInputSource
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Button _jumpButton;
        [SerializeField] private Button _pauseButton;

        public float HorizontalDirection => _joystick.Horizontal;
        public float VerticalDirection => _joystick.Vertical;
        public bool Jump { get; private set; }
        public bool Pause { get; private set; }

        public event Action InventoryRequested;
        public event Action QuestWindowRequested;
        public event Action SettingsMenuRequested;

        private void Awake() {
            _jumpButton.onClick.AddListener(() => Jump = true);
            _pauseButton.onClick.AddListener(() =>
            {
                if (!Pause)
                    InventoryRequested?.Invoke();
                Pause = true;
            });
        }

        private void OnDestroy()
        {
            _jumpButton.onClick.RemoveAllListeners();
            _pauseButton.onClick.RemoveAllListeners();
        }

        public void ResetOneTimeActions()
        {
            Jump = false;
            Pause = false;
        }
    }
}
