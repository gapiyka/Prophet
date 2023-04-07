using System;
using Items.Enum;
using UnityEngine;

namespace Items.Data
{
    [Serializable]
    public class FishItemDescriptor : ItemDescriptor
    {
        [field: SerializeField] public float SellPrice { get; private set; }
        [field: SerializeField] public float Saturation { get; private set; }

        public FishItemDescriptor(ItemId itemId, ItemType type, Sprite itemSprite, ItemRarity itemRarity,
            float buyPrice, float sellPrice, float saturation) :
            base(itemId, type, itemSprite, itemRarity, buyPrice)
        {
            SellPrice = sellPrice;
            Saturation = saturation;
        }
    }
}