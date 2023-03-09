using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    namespace ItemSystem
    {
        [System.Serializable]
        public class InventorySlot
        {
            public ItemStack ItemStack;
            public SOItemBase Item
            {
                get { return ItemStack.Item; }
                set { ItemStack.Item = value; }
            }
            public int Count
            {
                get { return ItemStack.Count; }
                set { ItemStack.Count = value; }
            }

            public InventorySlot(SOItemBase item, int count)
            {
                this.ItemStack.Item = item;
                this.ItemStack.Count = count;
            }
            public InventorySlot(ItemStack itemStack)
            {
                this.ItemStack = itemStack;
            }
            public InventorySlot()
            {
                this.ItemStack.Item = null;
                this.ItemStack.Count = 0;
            }

            public bool IsFull{get { return ItemStack.IsFull; } }
            public bool IsEmpty { get { return ItemStack.IsEmpty; } }

            public void Clear()
            {
                this.Item = null;
                this.Count = 0;
            }
            public int SetItem(SOItemBase item, int count)
            {
                this.Item = item;
                if (count >= item.StackLimit)
                {
                    this.Count = item.StackLimit;
                    return count - item.StackLimit;
                }
                else
                {
                    this.Count = count;
                    return 0;
                }
            }
            public int PutItem(int count)
            {
                int temp = count + Count;
                if (temp > Item.StackLimit)
                {
                    temp -= Item.StackLimit;
                    this.Count = Item.StackLimit;
                    return temp;
                }
                else
                    this.Count += count;
                return 0; //return 0 indicates that didn't exceed the limit
            }
            public int TakeItem(int count)
            {
                if (count >= this.Count)
                {
                    count -= this.Count;
                    Clear();
                    return count;
                }
                else
                {
                    this.Count -= count;
                    return 0;
                }
            }

            public static implicit operator ItemStack(InventorySlot slot)
            {
                return slot.ItemStack;
            }
            public static implicit operator int(InventorySlot slot)
            {
                return slot.ItemStack.Count;
            }
            public static explicit operator bool(InventorySlot slot)
            {
                return slot.ItemStack.IsEmpty;
            }

            public override string ToString()
            {
                return ItemStack.ToString();
            }
            public bool Equals(InventorySlot obj)
            {
                return obj.ItemStack.Equals(this.ItemStack);
            }
        }
    }
}
