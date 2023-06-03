using System;
using System.Collections.Generic;
using System.Linq;
using Items.Core;
using UnityEngine;

namespace Items
{
    public class Inventory
    {
        public const int InventorySize = 36;
        public List<Item> BackPackItems { get; }
        public FishingRod FishingRod { get; private set; }

        private readonly Transform _player;

        public event Action<Item, Vector2> ItemDropped;
        public event Action BackpackChanged;
        public event Action FishingRodChanged;

        public Inventory(List<Item> backPackItems, FishingRod fishingRod, Transform player)
        {
            FishingRod = fishingRod ?? null;

            if (backPackItems != null)
                return;

            BackPackItems = new List<Item>();
            for (var i = 0; i < InventorySize; i++)
                BackPackItems.Add(null);
            
            _player = player;
        }

        public bool TryAddToInventory(Item item)
        {
            if (item is FishingRod fishingRod && FishingRod == null && TryEquip(fishingRod))
                return true;

            return TryToAddToBackPack(item);
        }
        
        public bool TryEquip(Item item)
        {
            if (!(item is FishingRod fishingRod))
                return false;
            
            var oldFishingRod = FishingRod;

            if (BackPackItems.Contains(fishingRod))
            {
                var indexOfItem = BackPackItems.IndexOf(fishingRod);
                PlaceToBackPack(oldFishingRod, indexOfItem);
            }
            else TryToAddToBackPack(oldFishingRod);

            FishingRod = fishingRod;
            FishingRodChanged?.Invoke();
            return true;
        }

        private bool TryToAddToBackPack(Item item)
        {
            if (BackPackItems.All(slot => slot != null))
                return false;
            var index = BackPackItems.IndexOf(null);

            PlaceToBackPack(item, index);
            return true;
        }

        public void RemoveItem(Item item, bool toWorld)
        {
            if (item is FishingRod fishingRod && FishingRod == fishingRod)
                UnEquip();
            else
                RemoveFromBackPack(item);

            if (toWorld)
                ItemDropped?.Invoke(item, _player.position);
        }

        public void UseItem(Item item)
        {
            if (item == FishingRod)
                FishingRod.Use();
            
            if (item is Fish fish) {
                item.Use();
                RemoveItem(item, false);
                BackPackItems.Remove(item);
                BackpackChanged?.Invoke();
            }
        }

        public void MoveItemToPositionInBackPack(Item item, int place)
        {
            if (item is FishingRod fishingRod)
            {
                var backPackItem = BackPackItems[place];
                if (backPackItem != null)
                {
                    TryEquip(backPackItem);
                    return;
                }

                if (TryPlaceToBackPack(item, place))
                    UnEquip();

                return;
            }

            TryPlaceToBackPack(item, place);
        }

        private bool TryPlaceToBackPack(Item item, int index)
        {
            var oldItem = BackPackItems[index];
            if (BackPackItems.Contains(item))
            {
                var indexOfItem = BackPackItems.IndexOf(item);
                BackPackItems[indexOfItem] = oldItem;
            }
            else if (oldItem != null)
                return false;

            BackPackItems[index] = item;
            BackpackChanged?.Invoke();
            return true;
        }

        private void UnEquip()
        {
            FishingRod = null;
            FishingRodChanged?.Invoke();
        }
        private void PlaceToBackPack(Item item, int index)
        {
            BackPackItems[index] = item;
            BackpackChanged?.Invoke();
        }

        private void RemoveFromBackPack(Item item)
        {
            var index = BackPackItems.IndexOf(item);
            BackPackItems[index] = null;
            BackpackChanged?.Invoke();
        }
    }
}
