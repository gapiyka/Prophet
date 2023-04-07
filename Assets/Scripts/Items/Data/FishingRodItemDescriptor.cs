using System;
using Items.Enum;
using UnityEngine;

namespace Items.Data
{
    [Serializable]
    public class FishingRodItemDescriptor : ItemDescriptor
    {
        [field: SerializeField] public float FishingPower { get; private set; }
        [field: SerializeField] public int Durability { get; private set; }

        public FishingRodItemDescriptor(ItemId itemId, ItemType type, Sprite itemSprite, ItemRarity itemRarity,
            float buyPrice, float fishingPower, int durability) :
            base(itemId, type, itemSprite, itemRarity, buyPrice)
        {
            FishingPower = fishingPower;
            Durability = durability;
        }
    }
}