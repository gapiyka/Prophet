using System.Linq;
using Items;
using Items.Core;
using Items.Data;
using System.Collections.Generic;
using UI.Core;
using UI.InventoryUI.Element;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.InventoryUI
{
    public class InventoryScreenAdapter : ScreenController<InventoryScreenView>
    {
        private readonly Inventory _inventory;
        private readonly List<RarityDescriptor> _rarityDescriptors;
        private readonly Dictionary<ItemSlot, Item> _backPackSlots;
        private readonly Sprite _emptyBackSprite;
        
        private ItemSlot _focusedSlot;
        private Item _movingItem;

        public InventoryScreenAdapter(InventoryScreenView view, 
            Inventory inventory, List<RarityDescriptor> rarityDescriptors) : 
            base(view)
        {
            _inventory = inventory;
            _rarityDescriptors = rarityDescriptors;
            _backPackSlots = new Dictionary<ItemSlot, Item>();
        }

        public override void Initialize()
        {
            InitializeBackpack();
            _inventory.BackpackChanged += UpdateBackpack;
            View.CloseClicked += RequestClose;
            base.Initialize();  
        }

        public override void Complete()
        {
            ClearBackpack();
            _inventory.BackpackChanged -= UpdateBackpack;
            View.CloseClicked -= RequestClose;
            base.Complete();
        }
        
        private void UpdateBackpack()
        {
            ClearBackpack();
            InitializeBackpack();
        }
        
        private void ClearSlot(ItemSlot slot)
        {
            if (TryGetItem(slot, out var item))
                _inventory.RemoveItem(item, true);
        }
        
        private bool TryGetItem(ItemSlot slot, out Item item)
        {
            item = _backPackSlots[slot];
            return item != null;
        }
        
        private void ClearBackpack()
        {
            ClearSlots(_backPackSlots.Select(item => item.Key).ToList());
            _backPackSlots.Clear();
        }
        
        private void ClearSlots(List<ItemSlot> slots)
        {
            foreach (var slot in slots)
            {
                UnsubscribeSlotEvents(slot);
                slot.ClearItem(_emptyBackSprite);
            }
        }

        private void InitializeBackpack()
        {
            var backPack = View.ItemSlots;
            for (int i = 0; i < backPack.Count; i++)
            {
                var slot = backPack[i];
                var item = _inventory.BackPackItems[i];
                _backPackSlots.Add(slot, item);
                
                if (item == null) continue;
                
                slot.SetItem(item.Descriptor.ItemSprite, item.Descriptor.ItemSprite, 1);
                SubscribeToSlotEvents(slot);
            }
        }
        
        private void SubscribeToSlotEvents(ItemSlot slot)
        {
            slot.SlotClicked += UseSlot;
            // slot.SlotClearClicked += ClearSlot;
            //
            // #region DragAndDrop
            slot.SlotClickedDown += OnSlotDown;
            slot.DragStarted += OnDragStarted;
            slot.Dragged += OnDragged;
            slot.DragEnded += DragEnded;
            // #endregion
        }
        
        private void UnsubscribeSlotEvents(ItemSlot slot)
        {
            slot.SlotClicked -= UseSlot;

            // #region DragAndDrop
            slot.SlotClickedDown -= OnSlotDown;
            slot.DragStarted -= OnDragStarted;
            slot.Dragged -= OnDragged;
            slot.DragEnded -= DragEnded;
            // #endregion
        }
        
        private void DragEnded(ItemSlot slot, Vector2 position)
        {
            if (_focusedSlot != slot)
                return;

            var item = _movingItem;
            _focusedSlot = null;
            _movingItem = null;
            View.MovingImage.gameObject.SetActive(false);
            slot.SetItem(item.Descriptor.ItemSprite, item.Descriptor.ItemSprite, 1);

            if (!TryGetSlotOnPosition(position, out var anotherSlot))
            {
                ClearSlot(slot);
                return;
            }

            var newPlace = _backPackSlots.Keys.ToList().IndexOf(anotherSlot);
            _inventory.MoveItemToPositionInBackPack(item, newPlace);
        }

        private bool TryGetSlotOnPosition(Vector2 position, out ItemSlot itemSlot)
        {
            itemSlot = null;
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = position
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);
            if (results.Count < 1)
                return false;

            foreach (var result in results)
            {
                if (result.gameObject.TryGetComponent(out itemSlot))
                    return true;
            }

            return false;
        }

        private void OnSlotDown(ItemSlot slot)
        {
            if (_focusedSlot != null)
                return;

            _focusedSlot = slot;
        }

        private void UseSlot(ItemSlot slot)
        {
            if (slot == _focusedSlot)
            {
                if (TryGetItem(slot, out var item))
                    _inventory.UseItem(item);
            }

            _focusedSlot = null;
        }
        
        private void OnDragStarted(ItemSlot slot)
        {
            if (_focusedSlot != slot)
                return;

            slot.ClearItem(null);
            TryGetItem(slot, out _movingItem);
            View.MovingImage.gameObject.SetActive(true);
            View.MovingImage.sprite = _movingItem.Descriptor.ItemSprite;
        }

        private void OnDragged(ItemSlot slot, Vector2 position)
        {
            if (_focusedSlot != slot)
                return;

            View.MovingImage.rectTransform.position = position;
        }
    }
}
