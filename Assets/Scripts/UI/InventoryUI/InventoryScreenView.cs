using System;
using System.Collections.Generic;
using System.Linq;
using UI.Core;
using UI.InventoryUI.Element;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InventoryUI
{
    public class InventoryScreenView : ScreenView
    {
        ///[SerializeField] private Button _closeButton;

        [SerializeField] private Transform _backPackContainer;

        public List<ItemSlot> ItemSlots { get; private set; }

        //[field: SerializeField] public Image MovingImage { get; private set; }

        public event Action CloseClicked;

        private void Awake()
        {
            //_closeButton.onClick.AddListener(() => CloseClicked?.Invoke());
            //TODO: replace _closeButton with drag & drop outside inventory
            ItemSlots = GetComponentsInChildren<ItemSlot>().ToList();
        }

        private void OnDestroy()
        {
            //_closeButton.onClick.RemoveAllListeners();
            //TODO: after replacing this method will be unnecessary
        }
    }
}