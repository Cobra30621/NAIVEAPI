using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using NaiveAPI.EditorTool;
using static NaiveAPI.EditorTool.EditorGUITool;
using System.IO;

namespace NaiveAPI
{
    public class SONaiveApiSetting : ScriptableObject
    {
        public ItemSystemData _ItemSystemData;

        public static SONaiveApiSetting Get
        {
            get
            {
                var settings = AssetDatabase.LoadAssetAtPath<SONaiveApiSetting>(NaiveApiData.Path.ProjectSetting + "\\NaiveApiSetting.asset");
                if (settings == null)
                {
                    settings = CreateInstance<SONaiveApiSetting>();
                    AssetDatabase.CreateAsset(settings, NaiveApiData.Path.ProjectSetting + "\\NaiveApiSetting.asset");
                    AssetDatabase.SaveAssets();
                }
                return settings;
            }
        }
    }

    [System.Serializable]
    public class ItemSystemData
    {
        public List<string> ItemTags = new List<string>();
    }
    static class SONaiveApiSettingProvider
    {
        [SettingsProvider]
        public static SettingsProvider NaiveApiSettingProvider()
        {
            SONaiveApiSetting data = null;
            Color defaultColor = GUI.color;
            return new SettingsProvider("Project/NaiveAPI", SettingsScope.Project)
            {
                label = "NaiveAPI",
                activateHandler = (context, element) => {
                    data = SONaiveApiSetting.Get;
                },
                guiHandler = (context) =>
                {
                    GUILayoutUtility.GetRect(0, 20);
                    DividerLine("");
                    if (GUILayout.Button("Copy local temporaryCache path"))
                        GUIUtility.systemCopyBuffer = Application.temporaryCachePath;
                    if (GUIUtility.systemCopyBuffer == Application.temporaryCachePath)
                        GUILayout.Label("Copied !");
                    EditorUtility.SetDirty(data);
                }
            };
        }
    }

}

