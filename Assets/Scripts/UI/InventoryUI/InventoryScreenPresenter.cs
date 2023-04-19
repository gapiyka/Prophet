using Items;
using Items.Core;
using Items.Data;
using Items.Enum;
using System.Collections.Generic;
using UI.Core;
using UI.InventoryUI.Element;
using UnityEngine;

namespace UI.InventoryUI
{
    public class InventoryScreenPresenter : ScreenController<InventoryScreenView>
    {
        private readonly Inventory _inventory;
        private readonly List<RarityDescriptor> _rarityDescriptors;
        private readonly Dictionary<ItemSlot, Item> _backPackSlots;
        private readonly Sprite _emptyBackSprite;

        public InventoryScreenPresenter(InventoryScreenView view, 
            Inventory inventory, List<RarityDescriptor> rarityDescriptors) : 
            base(view)
        {
            _inventory = inventory;
            _rarityDescriptors = rarityDescriptors;
            _emptyBackSprite =
            _rarityDescriptors.Find(match: descriptor => descriptor.ItemRarity == ItemRarity.None).Sprite;
            _backPackSlots = new Dictionary<ItemSlot, Item>();
        }

        public override void Initialize()
        {
            View.CloseClicked += RequestClose;
            base.Initialize();
        }

        public override void Complete()
        {
            View.CloseClicked -= RequestClose;
            base.Complete();
        }
    }
}
