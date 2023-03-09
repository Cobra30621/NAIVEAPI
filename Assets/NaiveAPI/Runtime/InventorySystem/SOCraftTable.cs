using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NaiveAPI
{
    namespace ItemSystem
    {
        [CreateAssetMenu(menuName = "Naive API/Item System/CraftTable")]
        public class SOCraftTable : ScriptableObject
        {
            public CraftTable CraftTable = new CraftTable();
        }
    }
}

