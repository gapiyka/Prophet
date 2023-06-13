using Items.Data;
using Items.Enum;

namespace Items.Core
{
    public class FishingRod : Item
    {
        private readonly FishingRodItemDescriptor _itemDescriptor;
        public FishingRodType FishingRodType { get; private set; }

        //public override int Amount => -1;

        public FishingRod(ItemDescriptor descriptor, FishingRodType type) : base(descriptor)
        {
            _itemDescriptor = descriptor as FishingRodItemDescriptor;
            FishingRodType = type;
        }

        public override void Use()
        {
            // play animation of sweeping rod
            // (or cycle of sprites changing, using Sprite in _itemDescriptor)
        }
    }
}
