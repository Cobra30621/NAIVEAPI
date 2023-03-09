using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NaiveAPI
{
    namespace EditorTool
    {
        [CustomEditor(typeof(UIElementPainter<>), true)]
        public class UIElementPainterEditor : Editor
        {
            IElementPainter m_target;
            List<string> selectableTargets = new List<string>();
            int selectableParentIndex = 0;
            SerializedProperty targetName;
            SerializedProperty UIDocument;
            SerializedProperty visible;
            private void OnEnable()
            {
                m_target = target as IElementPainter;
                targetName = serializedObject.FindProperty("ElementName");
                UIDocument = serializedObject.FindProperty("uid");
                visible = serializedObject.FindProperty("Visible");
                if (m_target.UIDocument != null)
                    findElementTargets((UIDocument.objectReferenceValue as UIDocument).rootVisualElement, "");
                for(int i = 1; i < selectableTargets.Count; i++)
                {
                    if(selectableTargets[i] == targetName.stringValue)
                    {
                        selectableParentIndex = i;
                        break;
                    }
                }
                m_target.ApplySettings();
            }

            public override void OnInspectorGUI()
            {
                UIDocument.objectReferenceValue = (UIDocument)EditorGUILayout.ObjectField(UIDocument.objectReferenceValue,typeof(UIDocument),true);
                serializedObject.ApplyModifiedProperties();
                if (UIDocument.objectReferenceValue == null) return;
                base.OnInspectorGUI();
                selectableTargets.Clear();
                findElementTargets((UIDocument.objectReferenceValue as UIDocument).rootVisualElement, "");
                selectableParentIndex = EditorGUILayout.Popup("Target Element", selectableParentIndex, selectableTargets.ToArray());
                if(selectableParentIndex >= 0 && selectableParentIndex < selectableTargets.Count)
                    targetName.stringValue = selectableTargets[selectableParentIndex];
                visible.boolValue = EditorGUILayout.Toggle("Display", visible.boolValue);
                if (serializedObject.ApplyModifiedProperties())
                {
                    m_target.Initialize();
                    m_target.ApplySettings();
                }

                //EditorUtility.SetDirty(target);
            }

            public void findElementTargets(VisualElement root, string level)
            {
                if (root == null) return;
                if (root.name != null)
                    if(root.GetType() == m_target.TargetType)
                    selectableTargets.Add(level + root.name);
                foreach (var ve in root.Children())
                    findElementTargets(ve, level);
            }
        }
    }
}
