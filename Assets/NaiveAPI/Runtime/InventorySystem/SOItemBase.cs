using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NaiveAPI
{
    namespace ItemSystem
    {
        [CreateAssetMenu(menuName = "Naive API/Item System/ItemBase")]
        public class SOItemBase : ScriptableObject
        {
            public string DisplayName = "";
            public string Id = "";
            [ItemTag]
            public string ItemTag = "";
            public int StackLimit = 1;
            public Sprite Icon;
        }
    }
}
