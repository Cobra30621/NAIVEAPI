using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
    namespace ItemSystem
    {
        [CustomPropertyDrawer(typeof(InventorySlot), true)]
        public class InventorySlotDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("ItemStack"));
            }
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return 18;
            }
        }
    }
}
