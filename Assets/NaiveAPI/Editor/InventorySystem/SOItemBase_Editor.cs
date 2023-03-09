using NaiveAPI.ItemSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NaiveAPI
{
    namespace ItemSystemEditor
    {
        [CustomEditor(typeof(SOItemBase), true)]
        public class SOItemBase_Editor : UnityEditor.Editor
        {
            public SOItemBase Target
            {
                get
                {
                    return this.target as SOItemBase;
                }
            }
            private void OnEnable()
            {

            }
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                serializedObject.Update();

                if (GUI.changed)
                {
                    EditorUtility.SetDirty(target);
                }
                serializedObject.ApplyModifiedProperties();
            }
            public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height)
            {
                if (Target.Icon != null)
                {
                    Texture2D newIcon = new Texture2D(width, height);
                    EditorUtility.CopySerialized(Target.Icon.texture, newIcon);
                    return newIcon;
                }

                return base.RenderStaticPreview(assetPath, subAssets, width, height);
            }

        }
    }
}