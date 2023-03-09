using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NaiveAPI.ItemSystem
{
    [System.Serializable]
    public class LootBag
    {
        [Range(0, 100)]
        [SerializeField]
        private float percent;
        public float Percent
        {
            get
            {
                return percent;
            }
        }
        [SerializeField]
        private bool isLimited;
        public bool IsLimited
        {
            get
            {
                return isLimited;
            }
        }

        public Inventory Loots = new Inventory(10);

        public LootBag(bool isLimited)
        {
            this.isLimited = isLimited;
        }

        public LootBag(Inventory inventory, bool isLimited, float percent)
        {
            Loots = new Inventory(inventory.Count);
            for (int i = 0;i < inventory.Slots.Count; i++)
            {
                Loots.Slots[i].ItemStack = inventory.Slots[i].ItemStack;
            }
            Loots.CalCount();
            this.isLimited = isLimited;
            this.percent = percent;
        }

        public LootBag(List<ItemStack> itemStacks, bool isLimited)
        {
            for (int i = 0;i < itemStacks.Count; i++)
            {
                Loots.Slots[i] = new InventorySlot(itemStacks[i]);
            }
            this.isLimited = isLimited;
        }

        public void DeleteEmptySlot()
        {
            for (int i = 0; i< Loots.Slots.Count; i++)
            {
                //if (Inventory.Slots[i].IsEmpty)
                    //Inventory.DeleteSlot(i);
            }
        }

        public SOItemBase Get()
        {
            if (isLimited && AllItemsEmpty()) return null;
            int index;
            do
            {
                index = Random.Range(0, Loots.Count);
            } while (Loots.Slots[index].Item == null);
            SOItemBase item = Loots.Slots[index].Item;
            if (isLimited)
                Loots.PopAt(index, 1);
            return item;
        }

        public bool AllItemsEmpty()
        {
            for (int i = 0; i < Loots.Count; i++)
            {
                if (!Loots.Slots[i].IsEmpty) return false;
            }
            return true;
        }
    }
}
