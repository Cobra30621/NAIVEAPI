using NaiveAPI.EditorTool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using NaiveAPI.ItemSystem;

namespace NaiveAPI
{
    namespace ItemSystemEditor
    {
        [CustomPropertyDrawer(typeof(ItemTagAttribute))]
        public class ItemTagDrawer : PropertyDrawer
        {
            private static bool isOpenTagEditor;
            private ItemTagAttribute Attribute { get { return attribute as ItemTagAttribute; } }
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                // Get project's ItemTags
                List<string> itemTags = new List<string>();
                if (SONaiveApiSetting.Get._ItemSystemData.ItemTags != null)
                    itemTags.AddRange(SONaiveApiSetting.Get._ItemSystemData.ItemTags);

                // If open TagEditor change color to WarningColor
                EditorGUITool.ColorRegion(isOpenTagEditor, EditorGUITool.ColorSet.Warning, Color.white, () =>
                {

                    // Calculate SelectIndex
                    int value = 1;
                    string[] propertyTags = property.stringValue.Split(",");
                    Attribute.SelectIndex = 0;
                    for (int i = 0; i < itemTags.Count; i++)
                    {
                        foreach (string tag in propertyTags)
                        {
                            if (tag == itemTags[i])
                                Attribute.SelectIndex += value;
                        }
                        value *= 2;
                    }
                    if (Attribute.SelectIndex == Mathf.Pow(2, itemTags.Count) - 1)
                        Attribute.SelectIndex = -1; // everything

                    // Main layout
                    position.width -= 50;
                    if (itemTags.Count != 0)
                        Attribute.SelectIndex = EditorGUI.MaskField(position, label.text, Attribute.SelectIndex, itemTags.ToArray());
                    else
                        EditorGUI.LabelField(position, label.text + "    -- There is No ItemTag --");
                    position.x = position.width + 20;
                    position.width = 40;
                    if (GUI.Button(position, "Edit"))
                    {
                        isOpenTagEditor = !isOpenTagEditor;
                    }
                    GUI.color = EditorGUITool.ColorSet.Default;
                });


                if (isOpenTagEditor)
                {
                    TagEditorLayout();
                    GUILayout.Space(10);
                }

                // caculate result
                property.stringValue = "";
                if (Attribute.SelectIndex == -1)
                {
                    for (int i = 0; i < itemTags.Count; i++)
                    {
                        property.stringValue += (property.stringValue == "" ? "" : ",") + itemTags[i];
                    }
                }
                else if (Attribute.SelectIndex != 0)
                {
                    property.stringValue = "";
                    List<int> bin = new List<int>();
                    for (int i = 0; Attribute.SelectIndex > 0; i++)
                    {
                        bin.Add(Attribute.SelectIndex % 2);
                        Attribute.SelectIndex = Attribute.SelectIndex / 2;
                    }
                    for (int i = 0; i < bin.Count; i++)
                    {
                        if (bin[i] == 1)
                            property.stringValue += (property.stringValue == "" ? "" : ",") + itemTags[i];
                    }
                }
            }

            #region tag editor layout
            private Vector2 scrollViewPosition;
            private int deleteCheck = -1;
            public void TagEditorLayout()
            {
                var setting = SONaiveApiSetting.Get;
                var itemTags = setting._ItemSystemData.ItemTags;
                var defaultColor = GUI.color;
                var totalRect = EditorGUITool.DividerLine("Tag Editor", 3, 25);
                GUILayout.Space(5);
                totalRect.y += 16;
                scrollViewPosition = GUILayout.BeginScrollView(scrollViewPosition);

                EditorGUITool.HorizontalGroup(() =>
                {
                    if (GUILayout.Button("New Tag", GUILayout.Width(150)))
                    {
                        itemTags.Add("NewTag");
                    }
                });

                for (int i = 0; i < itemTags.Count; i++)
                {
                    EditorGUITool.HorizontalGroup(() =>
                    {
                        itemTags[i] = GUILayout.TextField(itemTags[i], GUILayout.Width(150));
                        GUILayout.Space(20);
                        if (i != 0)
                        {
                            if (GUILayout.Button("¡¶", GUILayout.Width(25)))
                            {
                                string temp = itemTags[i - 1];
                                itemTags[i - 1] = itemTags[i];
                                itemTags[i] = temp;
                            }
                        }
                        else
                        {
                            GUILayout.Space(28);
                        }
                        if (i != itemTags.Count - 1)
                        {
                            if (GUILayout.Button("¡¿", GUILayout.Width(25)))
                            {
                                string temp = itemTags[i + 1];
                                itemTags[i + 1] = itemTags[i];
                                itemTags[i] = temp;
                            }
                        }
                        else
                        {
                            GUILayout.Space(28);
                        }
                        GUILayout.Space(20);

                        EditorGUITool.ColorRegion(deleteCheck == i, EditorGUITool.ColorSet.Danger, Color.white, () =>
                        {
                            if (GUILayout.Button("Delete", GUILayout.Width(50)))
                            {
                                if (deleteCheck == i)
                                {
                                    itemTags.RemoveAt(i);
                                    deleteCheck = -1;
                                }
                                else
                                    deleteCheck = i;
                            }
                        });

                        if (deleteCheck == i)
                        {
                            EditorGUITool.ColorRegion(EditorGUITool.ColorSet.Success, () =>
                            {
                                if (GUILayout.Button("Cancel", GUILayout.Width(50)))
                                {
                                    deleteCheck = -1;
                                }
                            });
                        }
                    });
                }
                var tempRect = GUILayoutUtility.GetLastRect();
                totalRect.height = tempRect.y + tempRect.height;
                GUILayout.EndScrollView();
                GUILayout.Space(3);
                totalRect.height = EditorGUITool.DividerLine("", 0).y - totalRect.y + 1;
                EditorGUITool.DrawRectangle(totalRect, Color.gray, 1.5f);
                EditorUtility.SetDirty(setting);
            }
            #endregion
        }
    }
}


