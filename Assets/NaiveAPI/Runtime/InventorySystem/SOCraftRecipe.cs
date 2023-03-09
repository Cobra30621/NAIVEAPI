using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    namespace ItemSystem
    {
        [CreateAssetMenu(menuName = "Naive API/Item System/CraftRecipe")]
        public class SOCraftRecipe : ScriptableObject
        {
            public List<ItemStack> Input = new List<ItemStack>();
            public List<ItemStack> Output = new List<ItemStack>();
        }
    }
}
