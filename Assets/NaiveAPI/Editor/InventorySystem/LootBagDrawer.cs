using NaiveAPI.ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
    namespace EditorTool
    {
        [CustomPropertyDrawer(typeof(LootBag), true)]
        public class LootBagDrawer : PropertyDrawer
        {
            Rect[] rect = new Rect[3];
            SerializedProperty percent;
            SerializedProperty isLimited;
            SerializedProperty loots;
            InventoryDrawer inventoryDrawer = new InventoryDrawer();
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                percent = property.FindPropertyRelative("percent");
                isLimited = property.FindPropertyRelative("isLimited");
                loots = property.FindPropertyRelative("Loots");

                rect[0] = position; rect[0].width = position.width - 80;rect[0].height = 20;
                rect[1] = rect[0]; rect[1].x = rect[0].xMax+10; rect[1].width = 60;
                rect[2] = position; rect[2].y+=20; rect[2].height -= 20;

                EditorGUIUtility.labelWidth = 20;
                percent.floatValue = EditorGUI.Slider(rect[0],new GUIContent("% "), percent.floatValue, 0, 1);
                EditorGUIUtility.labelWidth = 55;
                isLimited.boolValue = EditorGUI.Toggle(rect[1], new GUIContent("isLimited"), isLimited.boolValue);
                EditorGUI.PropertyField(rect[2], loots);
            }
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return 20 + inventoryDrawer.GetPropertyHeight(property.FindPropertyRelative("Loots"), new GUIContent())+25;
            }
        }
    }
}
