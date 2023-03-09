using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    namespace ItemSystem
    {

        // for SOItemBase
        [System.Serializable]
        public class Inventory
        {
            #region variable
            /// <summary>
            ///  provide two mode for searching specific item in inventory
            ///  contain:  if you give the tag string "a,b,c", return all objects which at least contains all tags(a,b,c).
            ///  anyOf: if you give the tag sting "a,b,c", return all objects which at least contains one of a or b or c
            /// </summary>
            public enum SearchMode{
                contain,
                anyOf
            }
            public event Action OnItemChanged;

            
            [SerializeField] public List<InventorySlot> Slots = new List<InventorySlot>();
            [SerializeField] private int size;
            [SerializeField] private int count;

            [SerializeField] private List<int> unUseindex = new List<int>(10);
            public Action CustomSort; 
            #endregion

            #region getter&setter
            /// <summary>
            //privatetory size
            /// </summary>
            public int Size { get { return size; } }  //璉
            /// <summary>
            /// how many slots been used
            /// </summary>
            public int Count { get => count; }
            #endregion

            #region Constructor
            public Inventory(int size)
            {
                this.size = size;
                Slots = new List<InventorySlot>();
                while (size-- > 0)
                    Slots.Add(new InventorySlot());

                OnItemChanged += CalCount;
            }
            #endregion

            #region function

            /// <summary>
            /// <para> change the size of inventorySlots,which means increase or decrease the number of Slots contained by your inventory</para>
            /// <para> if new size is bigger , it return null </para>
            /// <para> if new size is smaller, it return an array contains slots were removed </para>
            /// </summary>
            /// <param name="newSize"></param>
            /// <returns></returns>
            public ItemStack[] SetSize(int newSize)
            {
                if (newSize < 0) return null;
                if (newSize < size)
                {
                    ItemStack[] output = new ItemStack[Mathf.Max(count - newSize, 0)];
                    var temp = new List<InventorySlot>();
                    for (int i = 0; i < newSize; i++)
                    {
                        temp.Add(Slots[i]);
                    }
                    Slots = temp;
                    size = newSize;
                    return output;
                }
                else{
                    for (int i = size; i <newSize; i++) {
                        Slots.Add(new InventorySlot());
                    }
                    size = newSize;
                    return null;
                }
            }

            /// <summary>
            /// let slots become continous(without empty slots);
            /// </summary>
            public void SortEmpty() {
                for (int i = 0; i < Slots.Count; i++) {
                    if (!Slots[i].IsEmpty)
                    {
                        ItemStack items = Slots[i].ItemStack;
                        Slots[i].Clear();
                        Push(items);
                    }
                }
            }

            /// <summary>
            /// push item in to inventory
            /// </summary>
            /// <param name="item"></param>
            /// <param name="count"></param>
            /// <returns></returns>
            public ItemStack Push(SOItemBase item, int count = 1) { return Push(new ItemStack(item,count));}
            public ItemStack Push(ItemStack inputSlot)
            {
                unUseindex.Clear();
                for (int i = 0; i < Size; i++)
                {
                    if (Slots[i].IsEmpty)
                    {
                        unUseindex.Add(i);
                    }
                    else if (Slots[i].Item == inputSlot.Item && !Slots[i].IsFull)
                    {
                        inputSlot.Count = Slots[i].PutItem(inputSlot.Count);
                        if (inputSlot.Count <= 0)
                            break;
                    }
                }
                //indicate that can't find same item in inventory
                for (int i = 0; i < unUseindex.Count; i++)
                { 
                    inputSlot.Count = Slots[unUseindex[i]].SetItem(inputSlot.Item, inputSlot.Count);
                    if (inputSlot.Count <= 0)
                        break;
                }
                OnItemChanged?.Invoke();
                return inputSlot;
            }
            public ItemStack[] Push(ItemStack[] inputStacks) {
                List<ItemStack> output=new List<ItemStack>();
                ItemStack overflow;
                for (int i=0;i<inputStacks.Length;i++) {
                    overflow = this.Push(inputStacks[i]);
                    if (!overflow.IsEmpty) {
                        output.Add(overflow);
                    }
                }
                return output.ToArray();
            }

            /// <summary>
            /// push item into specific slots by index
            /// </summary>
            /// <param name="slot"></param>
            /// <param name="index"></param>
            /// <returns></returns>
            public ItemStack PushAt(SOItemBase item, int count, int index) { return PushAt(new ItemStack(item,count),index);}
            public ItemStack PushAt(ItemStack itemStack, int index)
            {
                if (Slots[index].IsFull) return itemStack;
                if (Slots[index].Item == itemStack.Item)
                {
                    itemStack.Count=Slots[index].PutItem(count);
                }else if (Slots[index].IsEmpty)
                {
                    itemStack.Count = Slots[index].SetItem(itemStack.Item, count);
                }
                OnItemChanged?.Invoke();
                return itemStack;
            }

            /// <summary>
            /// take item out from inventory
            /// </summary>
            /// <param name="item"></param>
            /// <param name="count"></param>
            public void Pop(SOItemBase item, int count) { Pop(new ItemStack(item, count)); }
            public void Pop(ItemStack itemStack)
            {
                for(int i = 0; i < Size; i++)
                {
                    if (Slots[i].Item == itemStack.Item)
                    {
                        itemStack.Count = Slots[i].TakeItem(itemStack.Count);
                        if (itemStack.IsEmpty) break;
                    }
                }
                OnItemChanged?.Invoke();
            }
            /// <summary>
            /// take specific slot's item out
            /// </summary>
            /// <param name="index"></param>
            /// <param name="count"></param>
            public void PopAt(int index,int count)
            {
                Slots[index].TakeItem(count);
                OnItemChanged?.Invoke();
            }

            /// <summary>
            /// if pop didn't success , return false;
            /// </summary>
            /// <param name="item"></param>
            /// <param name="count"></param>
            /// <returns></returns>
            public bool TryPop(SOItemBase item, int count) { return TryPop(new ItemStack(item, count));}
            public bool TryPop(ItemStack itemStack)
            {
                if (itemStack.Count > CountItem(itemStack.Item))
                    return false;
                Pop(itemStack);
                return true;
            }
            public bool TryPopAt(int index, int count)
            {
                if (Slots[index] > count)
                {
                    PopAt(index, count);
                    return true;
                }
                return false;
            }



            /// <summary>
            /// return value is a array of index of target item
            /// </summary>
            /// <returns></returns>
            public List<InventorySlot> FindItem(SOItemBase match)
            {
                List<InventorySlot> output = new List<InventorySlot>();
                for(int i = 0; i < Size; i++)
                {
                    if (Slots[i] == null) continue;
                    if (Slots[i].Item == match)
                        output.Add(Slots[i]);
                }
                return output;
            }

            //tags:a,b,c 
            /// <summary>
            /// contain: the item in the slot containˇs all tags in the input string
            /// anyOf: the item in the slot containˇs one of tag in the input string
            /// </summary>
            /// <param name="match"></param>
            /// <param name="mode"></param>
            /// <returns></returns>
            public List<int> FindItemByTag(string match,SearchMode mode=SearchMode.contain)
            {
                List<int> output = new List<int>();
                string[] targetStrings = match.Split(',');
                if (mode == SearchMode.contain)
                {
                    for(int i = 0; i < Size; i++)
                    {
                        bool contains = true;
                        string[] myTags = Slots[i].Item.ItemTag.Split(',');
                        List<String> myTagList = new List<string>(myTags.Length);
                        for (int j = 0; j < myTags.Length; j++)
                        {
                            myTagList[j] = myTags[j];
                        }
                        foreach (string tag in targetStrings)
                        {
                            if (!myTagList.Contains(tag))
                                contains = false;
                        }
                        if (contains) output.Add(i);
                    }
                }else if(mode == SearchMode.anyOf)
                {

                    for (int i = 0; i < Size; i++)
                    {
                        bool containsAny = false;
                        string[] myTags = Slots[i].Item.ItemTag.Split(',');
                        List<String> myTagList = new List<string>(myTags.Length);
                        for (int j = 0; j < myTags.Length; j++)
                        {
                            myTagList[j] = myTags[j];
                        }
                        foreach (string tag in targetStrings)
                        {
                            if (myTagList.Contains(tag))
                                containsAny = true;
                        }
                        if (containsAny)
                            output.Add(i);
                    }
                }
                return output;
            }

            /// <summary>
            /// return how many match items in inventory
            /// </summary>
            /// <param name="match"></param>
            /// <returns></returns>
            public int CountItem(SOItemBase match)
            {
                int output=0;
                foreach(var i in FindItem(match))
                    output+=i.Count;
                return output;
            }

            /// <summary>
            /// swap two slot in same inventory
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            public void Swap(int a,int b)
            {
                ItemStack temp = Slots[a];
                Slots[a].ItemStack = Slots[b].ItemStack;
                Slots[b].ItemStack = temp;
                OnItemChanged?.Invoke();
            }

            /// <summary>
            /// swap slot with another inventory
            /// </summary>
            /// <param name="target"></param>
            /// <param name="targetIndex"></param>
            /// <param name="selfIndex"></param>
            public void Swap(Inventory target,int targetIndex,int selfIndex)
            {
                InventorySlot temp = target.Slots[targetIndex];
                target.Slots[targetIndex]=Slots[selfIndex];
                Slots[selfIndex] = temp;
                OnItemChanged?.Invoke();
            }

            public void CalCount()
            {
                count = 0;
                for(int i = 0; i < Slots.Count; i++)
                {
                    if (Slots[i].Item != null)
                        count++;
                }
            }
            #endregion

        }
    }
}
