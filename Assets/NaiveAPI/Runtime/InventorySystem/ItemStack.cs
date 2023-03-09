using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    namespace ItemSystem
    {
        [System.Serializable]
        public struct ItemStack
        {
            public ItemStack(SOItemBase item, int count)
            {
                this.Item = item;
                this.Count = count;
            }
            public ItemStack(ItemStack itemStack)
            {
                this = itemStack;
            }

            public SOItemBase Item;
            public int Count;
            public bool IsFull
            {
                get
                {
                    if (Item == null)
                        return false;
                    else
                        return Count >= Item.StackLimit;
                }
            }
            public bool IsEmpty
            {
                get
                {
                    return Count == 0;
                }
            }

            public override string ToString()
            {
                return $"ItemStack: {Item.name}, {Count}";
            }
            public bool Equals(ItemStack obj)
            {
                return ((Item == obj.Item) && (Count == obj.Count));
            }
        }
    }
}
