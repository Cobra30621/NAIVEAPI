using NaiveAPI.ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
    namespace EditorTool
    {
        [CustomPropertyDrawer(typeof(LootPool), true)]
        public class LootPoolDrawer : PropertyDrawer
        {
            Rect[] rect = new Rect[5];
            Color defaultColor = GUI.color;
            LootBagDrawer LootBagDrawer = new LootBagDrawer();
            SerializedProperty lootBags;
            SerializedProperty behavior;
            GUIContent GUIContent = new GUIContent();
            bool deleteCheck = false;
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                lootBags = property.FindPropertyRelative("LootBags");
                behavior = property.FindPropertyRelative("behavior");

                rect[0] = position; rect[0].height = 20;

                property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(rect[0], property.isExpanded, label);
                EditorGUI.EndFoldoutHeaderGroup();

                if (!property.isExpanded) return;

                rect[0].NextY();
                behavior.enumValueIndex = (int)(LootPool.EmptyBehavior)EditorGUI.EnumPopup(rect[0], "Empty Behavior", (LootPool.EmptyBehavior)behavior.enumValueIndex);
                rect[0].y += 5;

                float totalPercent = 1;
                float percent;

                rect[1] = rect[0]; rect[1].OffsetX(20); rect[1].width -= 15;
                for (int i = 0; i < lootBags.arraySize; i++)
                {
                    var sp = lootBags.GetArrayElementAtIndex(i);
                    percent = totalPercent * sp.FindPropertyRelative("percent").floatValue;
                    if (percent == 0) GUI.color = EditorGUITool.ColorSet.Danger;
                    rect[1].NextY();
                    rect[1].height = 20;

                    Rect btnRect = rect[1];
                    btnRect.OffsetX(-22);
                    btnRect.OffsetY(2);
                    btnRect.width = 16;
                    btnRect.height = 16;
                    if (sp.isExpanded)
                    {
                        if (EditorGUITool.TextureButton(btnRect, EditorGUITool.Icon.DownArrow))
                        {
                            sp.isExpanded = false;
                        }
                        btnRect.NextY();
                        if (i > 0)
                            if (GUI.Button(btnRect, "↑", GUI.skin.label))
                            {
                                lootBags.MoveArrayElement(i, i - 1);
                            }
                        btnRect.NextY();
                        if (i < lootBags.arraySize - 1)
                            if (GUI.Button(btnRect, "↓", GUI.skin.label))
                            {
                                lootBags.MoveArrayElement(i, i + 1);
                            }
                    }
                    else
                    {
                        if (EditorGUITool.TextureButton(btnRect, EditorGUITool.Icon.RightArrow))
                        {
                            sp.isExpanded = true;
                        }
                    }
                    btnRect = rect[1];
                    btnRect.width = 60;
                    btnRect.height = 18;
                    btnRect.x = position.xMax - 60;
                    if (deleteCheck)
                    {
                        btnRect.x -= 70;
                        GUI.color = EditorGUITool.ColorSet.Danger;
                        if (GUI.Button(btnRect, "Delete"))
                        {
                            lootBags.DeleteArrayElementAtIndex(i);
                            i--;
                            deleteCheck = false;
                            continue;
                        }
                        GUI.color = defaultColor;
                        btnRect.x += 70;
                        EditorGUITool.ColorRegion(EditorGUITool.ColorSet.Success, () =>
                        {
                            if (GUI.Button(btnRect, "Cancel"))
                            {
                                deleteCheck = false;
                            }
                        });
                    }
                    else
                    {
                        if(GUI.Button(btnRect, "Delete")){
                            deleteCheck = true;
                        }
                    }

                    EditorGUI.LabelField(rect[1], percent!=0? 
                                                    $"Level: {i}  |  Real Percent: "+string.Format("{0:0.00}", percent * 100)+"%"
                                                   :"Error : 0% to this LootBag",GUI.skin.label);
                    totalPercent -= percent;
                    rect[1].NextY();
                    if (sp.isExpanded)
                    {
                        rect[1].OffsetX(10);
                        rect[1].height = LootBagDrawer.GetPropertyHeight(sp, new GUIContent());
                        LootBagDrawer.OnGUI(rect[1], sp, GUIContent);
                        rect[1].OffsetX(-10);
                    }
                    GUI.color = defaultColor;
                }
                rect[1].x = position.x+20;
                rect[1].NextY();
                rect[1].height = 18;

                if (totalPercent == 1) GUI.color = EditorGUITool.ColorSet.Danger;
                EditorGUI.LabelField(rect[1], "This pool have "+ string.Format("{0:0.00}", totalPercent * 100) +"% to get nothing");
                GUI.color = defaultColor;
                rect[1].NextY();
                if (GUI.Button(rect[1], "Add new LootBag")){
                    lootBags.InsertArrayElementAtIndex(lootBags.arraySize);
                }

                EditorGUI.DrawRect(new Rect(position.x - 10, position.y + 20, 5, position.height - 20), EditorGUITool.ColorSet.Success.A(.15f));
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                if (!property.isExpanded)
                    return 20;

                float output = 80;
                var sp = property.FindPropertyRelative("LootBags");
                for (int i = 0; i < sp.arraySize; i++)
                {
                    if (sp.GetArrayElementAtIndex(i).isExpanded)
                        output += LootBagDrawer.GetPropertyHeight(sp.GetArrayElementAtIndex(i), new GUIContent());
                    else
                        output += 20;
                    output += 20;
                }
                return output;
            }
        }
    }
}
