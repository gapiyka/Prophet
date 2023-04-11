using System;
using Items.Core;
using Items.Data;
using Items.Enum;

namespace Items
{
    public class ItemsFactory
    {
        public ItemsFactory() {}

        public Item CreateItem(ItemDescriptor descriptor)
        {
            switch (descriptor.Type)
            {
                case ItemType.Fish:
                    return new Fish(descriptor);
                case ItemType.FishingRod:
                    return new FishingRod(descriptor, GetRodType(descriptor));
                default:
                    throw new NullReferenceException($"Item type " +
                        $"{descriptor.Type} is not implemented yet");
            }
        }

        private FishingRodType GetRodType(ItemDescriptor descriptor)
        {
            switch (descriptor.ItemId)
            {
                case ItemId.LameFishingRod:
                    return FishingRodType.LameFishingRod;
                case ItemId.AverageFishingRod:
                    return FishingRodType.AverageFishingRod;
                case ItemId.CoolFishingRod:
                    return FishingRodType.CoolFishingRod;
                case ItemId.AwesomeFishingRod:
                    return FishingRodType.AwesomeFishingRod;
                default:
                    throw new NullReferenceException($"Fishing rod type for " +
                        $"{descriptor.ItemId} is not implemented yet");
            }
        }
    }
}
