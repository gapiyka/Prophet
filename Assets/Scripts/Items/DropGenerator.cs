using System.Collections.Generic;
using System.Linq;
using Core.Services.Updater;
using Items.Data;
using Items.Enum;
using Player;
using UnityEngine;

namespace Items
{
    public class DropGenerator 
    {
        private PlayerEntity _playerEntity;
        private List<ItemDescriptor> _itemsDescriptors;
        private ItemsSystem _itemsSystem;
        
        public DropGenerator(List<ItemDescriptor> descriptors, PlayerEntity playerEntity, ItemsSystem itemsSystem)
        {
            _playerEntity = playerEntity;
            _itemsDescriptors = descriptors;
            _itemsSystem = itemsSystem;
            ProjectUpdater.Instance.UpdateCalled += Update;
        }
        
        private void DropRandomItem(ItemRarity rarity)
        {
            List<ItemDescriptor> items = _itemsDescriptors.Where(item => item.ItemRarity == rarity).ToList();
            ItemDescriptor itemDescriptor = items[Random.Range(0, items.Count())];
            _itemsSystem.DropItem(itemDescriptor, _playerEntity.transform.position + Vector3.one);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.G)) DropRandomItem(GetDropRarity());
        }
        
        private ItemRarity GetDropRarity()
        {
            float chance = Random.Range(0, 100);
            return chance switch
            {
                <= 40 => ItemRarity.Trash,
                > 40 and <= 75 => ItemRarity.Common,
                > 75 and <= 90 => ItemRarity.Rare,
                > 90 and <= 97 => ItemRarity.Legendary,
                > 97 and <= 100 => ItemRarity.Epic,
                _ => ItemRarity.Trash
            };
        }
    }
}