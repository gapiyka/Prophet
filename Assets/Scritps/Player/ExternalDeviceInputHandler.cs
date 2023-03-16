using UnityEngine;

namespace Player
{
    public class ExternalDeviceInputHandler : IEntityInputSource
    {
        public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
        public float VerticalDirection => Input.GetAxisRaw("Vertical");
        public bool Jump { get; private set; }

        public void OnUpdate()
        {
            if (Input.GetButtonDown("Jump"))
                Jump = true;
        }

        public void ResetOneTimeActions() => Jump = false;
    }
}
