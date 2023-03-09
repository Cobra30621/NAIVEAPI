using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NaiveAPI
{
    namespace ItemSystem
    {
        [System.Serializable]
        public class CraftTable
        {
            public List<SOCraftRecipe> Recipes = new List<SOCraftRecipe>();

            public bool IterateAllRecipes(Func<SOCraftRecipe, int, bool> func)
            {
                for (int i = 0; i < Recipes.Count; i++)
                {
                    if (func.Invoke(Recipes[i], i))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}

