using NaiveAPI.EditorTool;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
    namespace MathRelated
    {
        [CustomPropertyDrawer(typeof(SecondOrderController), true)]
        public class SecondOrderControllerDrawer : PropertyDrawer
        {
            private bool isLimitedValue;
            private bool isEditMode;
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                SerializedProperty fp, zp, rp, sp;
                fp = property.FindPropertyRelative("f");
                zp = property.FindPropertyRelative("z");
                rp = property.FindPropertyRelative("r");
                sp = property.FindPropertyRelative("spd");
                float f, z, r, s;
                f = fp.floatValue;
                z = zp.floatValue;
                r = rp.floatValue;
                s = sp.floatValue;
                EditorGUI.BeginChangeCheck();
                SecondOrderController controller = new SecondOrderController(f, z, r, 0, s);
                controller = NaiveGUILayout.SecondOrderControllerField(position, label.text, controller,ref isEditMode,ref isLimitedValue);
                if (EditorGUI.EndChangeCheck())
                {
                    fp.floatValue = controller.F;
                    zp.floatValue = controller.Z;
                    rp.floatValue = controller.R;
                    sp.floatValue = controller.Spd;
                    property.FindPropertyRelative("twoPiF").floatValue = controller.TwoPiF;
                    property.FindPropertyRelative("d").floatValue = controller.D;
                    property.FindPropertyRelative("k1").floatValue = controller.K1;
                    property.FindPropertyRelative("k2").floatValue = controller.K2;
                    property.FindPropertyRelative("k3").floatValue = controller.K3;
                }
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                if (isEditMode)
                    return 286f;
                else
                    return 18f;
            }
        }
    }
}
