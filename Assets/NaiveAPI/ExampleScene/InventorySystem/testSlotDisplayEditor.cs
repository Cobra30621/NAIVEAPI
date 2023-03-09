using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(testSlotDisplay))]
public class testSlotDisplayEditor : Editor
{
    testSlotDisplay m_target;
    private void OnEnable()
    {
        m_target = target as testSlotDisplay;
    }
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if (EditorGUI.EndChangeCheck())
        {
            m_target.repaint();
            EditorUtility.SetDirty(target); // if you need to save serialize object
        }
    }
}
