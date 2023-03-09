using NaiveAPI.EditorTool;
using NaiveAPI.ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
    namespace ItemSystemEditor
    {
        [CustomEditor(typeof(SOCraftRecipe))]
        public class SOCraftRecipe_Editor : UnityEditor.Editor
        {
            SOCraftRecipe Target;
            SerializedProperty inputs;
            SerializedProperty outputs;
            private void OnEnable()
            {
                Target = target as SOCraftRecipe;
            }
            public override void OnInspectorGUI()
            {
                inputs = serializedObject.FindProperty("Input");
                outputs = serializedObject.FindProperty("Output");
                EditorGUILayout.Space(10);
                EditorGUITool.DividerLine("Input");
                EditorGUILayout.Space(5);
                for (int i = 0; i <inputs.arraySize; i++) 
                {
                    EditorGUITool.HorizontalGroup(() =>
                    {
                        EditorGUILayout.PropertyField(inputs.GetArrayElementAtIndex(i));
                        EditorGUITool.ColorRegion(EditorGUITool.ColorSet.Danger, () =>
                        {
                            if (GUILayout.Button("-",GUILayout.Height(20),GUILayout.Width(20)))
                            {
                                inputs.DeleteArrayElementAtIndex(i);
                                i--;
                            }
                        });
                    });
                }
                EditorGUITool.HorizontalGroup(() =>
                {
                    EditorGUILayout.LabelField("", GUILayout.Width(20));
                    if (GUILayout.Button("+ element", GUILayout.Width(80), GUILayout.Height(18)))
                    {
                        inputs.InsertArrayElementAtIndex(inputs.arraySize);
                    }
                });


                // output
                EditorGUILayout.Space(20);
                EditorGUITool.DividerLine("Output");
                EditorGUILayout.Space(5);
                for (int i = 0; i < outputs.arraySize; i++)
                {
                    EditorGUITool.HorizontalGroup(() =>
                    {
                        EditorGUILayout.PropertyField(outputs.GetArrayElementAtIndex(i));
                        EditorGUITool.ColorRegion(EditorGUITool.ColorSet.Danger, () =>
                        {
                            if (GUILayout.Button("-", GUILayout.Height(20), GUILayout.Width(20)))
                            {
                                outputs.DeleteArrayElementAtIndex(i);
                                i--;
                            }
                        });
                    });
                }
                EditorGUITool.HorizontalGroup(() =>
                {
                    EditorGUILayout.LabelField("", GUILayout.Width(20));
                    if (GUILayout.Button("+ element", GUILayout.Width(80), GUILayout.Height(18)))
                    {
                        outputs.InsertArrayElementAtIndex(outputs.arraySize);
                    }
                });

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
