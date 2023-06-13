using Items.Data;
using UnityEngine;

namespace Items.Scriptable
{
    [CreateAssetMenu(fileName = "Fish", menuName = "ItemsSystem/Fish")]
    public class FishItemScriptable : BaseItemScriptable
    {
        [SerializeField] private FishItemDescriptor _fishItemDescriptor;

        public override ItemDescriptor ItemDescriptor => _fishItemDescriptor;

        public FishItemScriptable(FishItemDescriptor descriptor)
        {
            _fishItemDescriptor = descriptor;
        }
    }
}