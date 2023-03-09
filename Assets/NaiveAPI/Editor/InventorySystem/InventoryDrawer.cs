using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using NaiveAPI.EditorTool;
using NaiveAPI.ItemSystem;

namespace NaiveAPI
{
    namespace EditorTool
    {
        [CustomPropertyDrawer(typeof(Inventory), true)]
        public class InventoryDrawer : PropertyDrawer
        {
            Rect[] rect = new Rect[10];
            SerializedProperty slots;
            SerializedProperty count;
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                slots = property.FindPropertyRelative("Slots");
                count = property.FindPropertyRelative("count");
                rect[0] = position;rect[0].height = 18;rect[0].width = EditorGUIUtility.labelWidth;
                rect[1] = new Rect(rect[0].x + 35, rect[0].y +18, position.width - 40, position.height - rect[0].height);
                rect[2] = new Rect(rect[0].xMax+5, rect[0].y, position.width - rect[0].width - 15, 18);

                EditorGUI.BeginChangeCheck();
                EditorGUI.LabelField(rect[0], label);
                EditorGUI.PropertyField(rect[1], slots,true);
                EditorGUI.LabelField(rect[2], $"Size: {slots.arraySize}  |  Count: {count.intValue}");
                if (EditorGUI.EndChangeCheck())
                {
                    property.FindPropertyRelative("size").intValue = slots.arraySize;
                    int sum = 0;
                    for(int i=0; i< slots.arraySize; i++)
                    {
                        if (slots.GetArrayElementAtIndex(i).FindPropertyRelative("ItemStack").FindPropertyRelative("Count").intValue != 0)
                            sum++;
                    }
                    count.intValue = sum;
                }
            }
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                var slots = property.FindPropertyRelative("Slots");
                return slots.isExpanded ? 20 * slots.arraySize + 70: 38;
            }
        }
    }
}
