using System.Collections.Generic;
using Items.Behaviour;
using Items.Core;
using Items.Data;
using Items.Interface.Rarity;
using UnityEngine;

namespace Items
{
    public class ItemsSystem
    {
        private readonly SceneItem _sceneItem;
        private readonly Transform _transform;
        private readonly List<IItemRarityColor> _colors;
        private readonly Dictionary<SceneItem, Item> _itemsOnScene;
        private readonly LayerMask _whatIsPlayer;
        private readonly ItemsFactory _itemsFactory;

        private readonly Inventory _inventory;

        public ItemsSystem(List<IItemRarityColor> colors, LayerMask whatIsPlayer, ItemsFactory itemsFactory, Inventory inventory)
        {
            _sceneItem = Resources.Load<SceneItem>($"{nameof(ItemsSystem)}/{nameof(SceneItem)}");
            _itemsOnScene = new Dictionary<SceneItem, Item>();

            GameObject gameObject = new GameObject();
            gameObject.name = nameof(ItemsSystem);
            _transform = gameObject.transform;
            _colors = colors;
            _whatIsPlayer = whatIsPlayer;
            _itemsFactory = itemsFactory;
            _inventory = inventory;
            _inventory.ItemDropped += DropItem;
        }

        public void DropItem(ItemDescriptor descriptor, Vector2 position) =>
            DropItem(_itemsFactory.CreateItem(descriptor), position);

        private void DropItem(Item item, Vector2 position)
        {
            SceneItem sceneItem = Object.Instantiate(_sceneItem, _transform);
            sceneItem.SetItem(item.Descriptor.ItemSprite, item.Descriptor.ItemId.ToString(), 
                _colors.Find(color => color.ItemRarity == item.Descriptor.ItemRarity).Color);
            sceneItem.PlayDrop(position);
            sceneItem.ItemClicked += TryPickSceneItem;
            _itemsOnScene.Add(sceneItem, item);
        }

        private void TryPickSceneItem(SceneItem sceneItem)
        {
            Collider2D player = Physics2D.OverlapCircle(sceneItem.Position, sceneItem.InteractionDistance, _whatIsPlayer);
            if (player == null) return;
            
            Item item = _itemsOnScene[sceneItem];
            if (!_inventory.TryAddToInventory(item))
                return;
            
            Debug.Log($"Item ({item.Descriptor.ItemId}) clicked, adding to inventory...");
            _itemsOnScene.Remove(sceneItem);
            sceneItem.ItemClicked -= TryPickSceneItem;
            Object.Destroy(sceneItem.gameObject);
        }
    }
}