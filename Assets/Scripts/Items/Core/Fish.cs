using Items.Data;
using UnityEngine;

namespace Items.Core
{
    public class Fish : Item
    {
        private FishItemDescriptor _itemDescriptor;
        private int _amount;
        //public override int Amount => _amount;

        public Fish(ItemDescriptor descriptor) : base(descriptor)
        {
            _itemDescriptor = descriptor as FishItemDescriptor;
            //_amount = 1;
        }

        public override void Use()
        {
            //_amount--;
            Debug.Log("Yummy, nyam nyam. . .");
            //if (_amount <= 0)
            Destroy();
        }

        private void Destroy()
        {
            throw new System.NotImplementedException();
        }

        // public void AddToStack(int amount)
        // {
        //     _amount += amount;
        // }
    }
}
