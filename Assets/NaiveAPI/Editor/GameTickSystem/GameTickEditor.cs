using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using NaiveAPI.GameTickSystem;
using static NaiveAPI.EditorTool.EditorGUITool;

namespace NaiveAPI
{
    namespace GameTickEditor
    {
        [CustomEditor(typeof(GameTick))]
        public class GameTickEditor : UnityEditor.Editor
        {
            GameTick m_target;
            SerializedProperty tickPerSec;
            SerializedProperty tickRate;
            SerializedProperty currentRealTick;
            SerializedProperty currentTick;
            SerializedProperty currentTime;
            private void OnEnable()
            {
                m_target = target as GameTick;
                tickPerSec = serializedObject.FindProperty("tickPerSec");
                tickRate = serializedObject.FindProperty("tickRate");
                currentRealTick = serializedObject.FindProperty("currentRealTick");
                currentTick = serializedObject.FindProperty("currentTick");
                currentTime = serializedObject.FindProperty("currentTime");
            }
            public override void OnInspectorGUI()
            {
                DividerLine("Settings", ColorSet.DarkGray, ColorSet.Default, 2);
                EditorGUILayout.LabelField("", GUILayout.Height(3));
                int newTickRate = tickRate.intValue;
                HorizontalGroup(() =>
                {
                    EditorGUILayout.LabelField("GameSpeed:  " + tickRate.intValue / (float)tickPerSec.intValue,GUILayout.Width(185));

                    if (GUILayout.Button("0.2",GUILayout.Height(18)))
                        newTickRate = (int)(tickPerSec.intValue * 0.2f);
                    if (GUILayout.Button("0.5", GUILayout.Height(18)))
                        newTickRate = (int)(tickPerSec.intValue * 0.5f);
                    if (GUILayout.Button("1", GUILayout.Height(18)))
                        newTickRate = tickPerSec.intValue;
                    if (GUILayout.Button("2", GUILayout.Height(18)))
                        newTickRate = (int)(tickPerSec.intValue * 2f);
                    if (GUILayout.Button("3", GUILayout.Height(18)))
                        newTickRate = (int)(tickPerSec.intValue * 3f);

                });

                if (newTickRate != tickRate.intValue)
                {
                    if(Application.isPlaying)
                        GameTick.TickRate = newTickRate;
                    tickRate.intValue = newTickRate;
                }
                
                HorizontalGroup(() =>
                {
                    EditorGUIUtility.labelWidth = 78;
                    if (Application.isPlaying)
                    {
                        EditorGUILayout.LabelField("Tick Per Sec:  " + GameTick.TickPerSec);
                        EditorGUILayout.LabelField("", GUILayout.Width(8));
                        EditorGUIUtility.labelWidth = 60;
                        newTickRate = EditorGUILayout.IntField("Tick Rate", GameTick.TickRate);
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(tickPerSec);
                        EditorGUILayout.LabelField("", GUILayout.Width(8));
                        EditorGUIUtility.labelWidth = 60;
                        EditorGUILayout.PropertyField(tickRate);
                    }
                });
                if (newTickRate != tickRate.intValue)
                {
                    if(Application.isPlaying)
                        GameTick.TickRate = newTickRate;
                    tickRate.intValue = newTickRate;
                }

                EditorGUILayout.LabelField("", GUILayout.Height(15));
                DividerLine("State",ColorSet.DarkGray,ColorSet.Default,2);
                HorizontalGroup(() =>
                {
                    EditorGUILayout.LabelField($"Current RealTick:  {currentRealTick.intValue}", GUILayout.Width(180));
                    EditorGUILayout.LabelField("", GUILayout.Width(8));
                    EditorGUILayout.LabelField($"TickRate:  {currentTick.intValue}", GUILayout.Width(180));
                });
                HorizontalGroup(() =>
                {
                    EditorGUILayout.LabelField($"Current Time       :  {decimal.Round((decimal)currentTime.floatValue,3)}", GUILayout.Width(180));
                });

                if (!Application.isPlaying)
                    serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
