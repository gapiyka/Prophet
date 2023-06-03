using Items.Data;
using UnityEngine;

namespace Items.Core
{
    public class Fish : Item
    {
        private FishItemDescriptor _itemDescriptor;
        private int _amount;

        public Fish(ItemDescriptor descriptor) : base(descriptor)
        {
            _itemDescriptor = descriptor as FishItemDescriptor;
        }

        public override void Use()
        {
            Debug.Log("Yummy " + _itemDescriptor.ItemId.ToString() + ", nyam nyam. . .");
        }
    }
}
