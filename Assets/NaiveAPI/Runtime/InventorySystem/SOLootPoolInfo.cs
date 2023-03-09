using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NaiveAPI.ItemSystem
{
    [CreateAssetMenu(menuName = "Naive API/Item System/LootPool")]
    public class SOLootPoolInfo : ScriptableObject
    {
        public LootBag[] lootBags;
        public LootPool.EmptyBehavior EmptyBehavior;
    }
}