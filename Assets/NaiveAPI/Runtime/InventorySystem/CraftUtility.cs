using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    namespace ItemSystem
    {
        public static class CraftUtility
        {
            static public ItemStack[] TryCraft(this Inventory inventory, SOCraftRecipe recipe, bool isPushResultIntoInventory = true)
            {
                if (IsAllowCraft(inventory, recipe))
                    return Craft(inventory, recipe, isPushResultIntoInventory);
                return null;
            }
            static public ItemStack[] Craft(this Inventory inventory, SOCraftRecipe recipe,bool isPushResultIntoInventory=true)
            {
                foreach (var i in recipe.Input)
                {
                    inventory.Pop(new InventorySlot(i.Item,i.Count));
                }
                if (isPushResultIntoInventory)
                    return inventory.Push(recipe.Output.ToArray());
                else
                    return recipe.Output.ToArray();
            }
            static public bool IsAllowCraft(this Inventory inventory, SOCraftRecipe recipe)
            {
                bool allowCraft = true;
                foreach (var i in recipe.Input)
                {
                    if (inventory.CountItem(i.Item) < i.Count)
                    {
                        allowCraft = false;
                        break;
                    }
                }
                return allowCraft;
            }
        }
    }
}
