using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaiveAPI
{
    [CreateAssetMenu(menuName = "Naive API/Item System/UI Theme/InventorySlot")]
    public class SOInventorySlotTheme : ScriptableObject
    {
        public Sprite BackGroundImage;
        public float IconSize = 100;
        public Color CountTextBackground = Color.clear;
        public Font CountTextFont;
    }
}
