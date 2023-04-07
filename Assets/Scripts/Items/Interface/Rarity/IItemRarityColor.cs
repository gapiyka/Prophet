using UnityEngine;
using Items.Enum;

namespace Items.Interface.Rarity
{
    public interface IItemRarityColor
    {
        ItemRarity ItemRarity { get; }
        Color Color { get; }
    }
}