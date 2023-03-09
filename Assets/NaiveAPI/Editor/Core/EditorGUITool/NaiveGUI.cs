using NaiveAPI.ItemSystem;
using NaiveAPI.MathRelated;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace NaiveAPI
{
    namespace EditorTool
    {
        public static class NaiveGUI
        {
            public static SecondOrderController SecondOrderControllerField(Rect position, string label, SecondOrderController controller, ref bool isEditMode, ref bool isLimitedValue)
            {
                if (isEditMode)
                {
                    Rect[] rect = new Rect[12];
                    rect[0] = new Rect(position.x + 18, position.y, EditorGUIUtility.labelWidth, 18f); //label
                    rect[1] = new Rect(position.x + 70, rect[0].position.y + 18, position.width - 70, 18f); // F bar
                    rect[2] = new Rect(position.x + 70, rect[1].position.y + 18, position.width - 70, 18f); // Z bar
                    rect[3] = new Rect(position.x + 70, rect[2].position.y + 18, position.width - 70, 18f); // R bar
                    rect[4] = new Rect(position.x + 70, rect[3].position.y + 18, position.width - 70, 18f); // spd field
                    rect[5] = rect[1]; rect[5].x -= 34; rect[5].width = 34;
                    rect[6] = rect[2]; rect[6].x -= 34; rect[6].width = 34;
                    rect[7] = rect[3]; rect[7].x -= 34; rect[7].width = 34;
                    rect[8] = rect[4]; rect[8].x -= 34; rect[8].width = 34;

                    rect[9] = new Rect(position.x + 36, rect[8].position.y + 27, position.width - 36, 180); // preview graph

                    rect[10] = new Rect(position.xMax - 50, position.y, 50, 18);// reset button;
                    rect[11] = new Rect(position.xMax - 130, position.y, 80, 18);// limited change;

                    EditorGUI.LabelField(rect[0], label);

                    float f, z, r, s;
                    EditorGUI.LabelField(rect[5], "F");
                    EditorGUI.LabelField(rect[6], "Z");
                    EditorGUI.LabelField(rect[7], "R");
                    EditorGUI.LabelField(rect[8], "Spd");
                    if (isLimitedValue)
                    {
                        f = EditorGUI.Slider(rect[1], controller.F, 0.005f, 0.1f);
                        z = EditorGUI.Slider(rect[2], controller.Z, 0f, 1f);
                        r = EditorGUI.Slider(rect[3], controller.R, -1.75f, 1.75f);
                        s = EditorGUI.Slider(rect[4], controller.Spd, 0.1f, 5f);
                    }
                    else
                    {
                        f = EditorGUI.FloatField(rect[1], controller.F);
                        z = EditorGUI.FloatField(rect[2], controller.Z);
                        r = EditorGUI.FloatField(rect[3], controller.R);
                        s = EditorGUI.FloatField(rect[4], controller.Spd);
                    }
                    if (GUI.Button(rect[10], "reset"))
                        controller = new SecondOrderController();
                    else
                        controller = new SecondOrderController(f, z, r, 0, s);
                    GUI.DrawTexture(rect[9], controller.GetGraph(new Vector2Int(400, 200)));
                    isLimitedValue = GUI.Toggle(rect[11], isLimitedValue, "Limited");
                }
                else
                {
                    Rect[] rect = new Rect[6];
                    rect[0] = new Rect(position.x + 18, position.y, EditorGUIUtility.labelWidth, 18f); //label
                    float valueWidth = (position.width - rect[0].width - 18) / 4f;
                    rect[1] = new Rect(rect[0].xMax, position.y, valueWidth, 18f); // F
                    rect[2] = new Rect(rect[1].xMax, position.y, valueWidth, 18f); // Z
                    rect[3] = new Rect(rect[2].xMax, position.y, valueWidth, 18f); // R
                    rect[4] = new Rect(rect[3].xMax, position.y, valueWidth, 18f); // Spd
                    rect[5] = new Rect(position.x + 3, position.y + 3, 12f, 12f); // edit button

                    EditorGUI.LabelField(rect[0], label);
                    EditorGUI.LabelField(rect[1], "F " + controller.F);
                    EditorGUITool.ColorRegion(new Color(.8f, .8f, .8f), () =>
                    {
                        EditorGUI.LabelField(rect[2], "Z " + controller.Z);
                    });
                    EditorGUI.LabelField(rect[3], "R " + controller.R);
                    EditorGUITool.ColorRegion(new Color(.8f, .8f, .8f), () =>
                    {
                        EditorGUI.LabelField(rect[4], "spd " + controller.Spd);
                    });
                } // not editting display
                if (EditorGUITool.TextureButton(new Rect(position.x + 3, position.y + 3, 12f, 12f), isEditMode, EditorGUITool.Icon.DownArrow, EditorGUITool.Icon.RightArrow))
                {
                    isEditMode = !isEditMode;
                }

                return controller;
            }

            public static ItemStack ItemStackField(Rect position, GUIContent label, ItemStack itemStack)
            {
                Rect[] rect = new Rect[5];
                rect[0] = position; rect[0].width = GUI.skin.label.CalcSize(new GUIContent(label)).x;
                rect[1] = new Rect(rect[0].xMax + 3, rect[0].y, 18, 18);
                rect[2] = new Rect(rect[1].xMax + 3, rect[0].y, (position.width - 160 - rect[0].width), 18);
                rect[3] = new Rect(rect[2].xMax + 5, rect[0].y, 75, 18);
                rect[4] = new Rect(rect[3].xMax + 15, rect[0].y, 35, 18);

                EditorGUI.LabelField(rect[0], label);
                if (itemStack.Item != null) if (itemStack.Item.Icon != null)
                        GUI.DrawTexture(rect[1], itemStack.Item.Icon.texture);
                itemStack.Item = (SOItemBase)EditorGUI.ObjectField(rect[2], itemStack.Item, typeof(ItemStack), false);
                itemStack.Count = EditorGUI.IntField(rect[3], itemStack.Count);
                if (GUI.Button(rect[4], "Clr"))
                {
                    itemStack.Item = null;
                    itemStack.Count = 0;
                }

                return itemStack;
            }


        }
    }
}
