using NaiveAPI.ItemSystem;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
    namespace ItemSystemEditor
    {
        [CustomPropertyDrawer(typeof(ItemStack), true)]
        public class ItemStackDrawer : PropertyDrawer
        {
            Rect[] rect = new Rect[5];
            GUIContent GUIContent = new GUIContent();
            SerializedProperty item;
            SerializedProperty count;
            // Draw the property inside the given rect
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                #region var
                item = property.FindPropertyRelative("Item");
                count = property.FindPropertyRelative("Count");

                rect[0] = position; rect[0].width = GUI.skin.label.CalcSize(label).x;
                rect[1] = new Rect(rect[0].xMax+3, rect[0].y, 18, 18);
                rect[2] = new Rect(rect[1].xMax+3, rect[0].y, (position.width - 140 - rect[0].width), 18);
                rect[3] = new Rect(rect[2].xMax+5, rect[0].y, 55, 18);
                rect[4] = new Rect(rect[3].xMax+15, rect[0].y, 35, 18);
                #endregion

                #region layout
                EditorGUI.LabelField(rect[0], label);
                SOItemBase icon  = (SOItemBase)item.objectReferenceValue;
                if(icon != null) if(icon.Icon != null)
                    GUI.DrawTexture(rect[1], icon.Icon.texture);
                EditorGUI.PropertyField(rect[2], item, GUIContent);
                EditorGUI.PropertyField(rect[3], count, GUIContent);
                if (GUI.Button(rect[4], "Clr"))
                {
                    item.objectReferenceValue = null;
                    count.intValue = 0;
                }
                #endregion
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return 18;
            }
        }
    }
}
