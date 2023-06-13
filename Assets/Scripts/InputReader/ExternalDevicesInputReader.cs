using System;
using Core.Services.Updater;
using UnityEngine;

namespace InputReader
{
    public class ExternalDevicesInputReader : IEntityInputSource, IDisposable, IWindowsInputSource
    {
        public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
        public float VerticalDirection => Input.GetAxisRaw("Vertical");
        public bool Jump { get; private set; }
        public bool Pause { get; private set; }

        public event Action InventoryRequested;
        public event Action QuestWindowRequested;
        public event Action SettingsMenuRequested;

        public ExternalDevicesInputReader()
        {
            ProjectUpdater.Instance.UpdateCalled += OnUpdate;
            ProjectUpdater.Instance.LateUpdateCalled += OnLateUpdate;
        }

        public void ResetOneTimeActions()
        {
            Jump = false;
            Pause = false;
        }

        public void Dispose()
        {
            ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
            ProjectUpdater.Instance.LateUpdateCalled -= OnLateUpdate;
        }

        private void OnUpdate()
        {
            if (Input.GetButtonDown("Jump"))
                Jump = true;
        }

        private void OnLateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(!Pause) 
                    InventoryRequested?.Invoke();
                Pause = true;
            } 
        }
    }
}