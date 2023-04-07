using Items.Data;
using UnityEngine;

namespace Items.Scriptable
{
    [CreateAssetMenu(fileName = "Item", menuName="ItemsSystem/FishingRod")]
    public class FishingRodScriptable : BaseItemScriptable
    {
        [SerializeField] private FishingRodItemDescriptor _fishingRodItemDescriptor;

        public override ItemDescriptor ItemDescriptor => _fishingRodItemDescriptor;

        public FishingRodScriptable(FishingRodItemDescriptor descriptor)
        {
            _fishingRodItemDescriptor = descriptor;
        }
    }
}