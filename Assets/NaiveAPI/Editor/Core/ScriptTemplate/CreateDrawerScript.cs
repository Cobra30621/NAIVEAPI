using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace NaiveAPI
{
    namespace EditorTool
    {
        public class CreateDrawerScript : UnityEditor.Editor
        {
            [MenuItem("Assets/Create/Naive API/ScriptTemplate/Drawer Script")]
            static void CreateExampleAssets()
            {
                Texture2D icon = EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

                var endNameEditAction = CreateInstance<DoCreateDrawerScript>();

                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, endNameEditAction, "NewDrawer.cs", icon, "");
            }
        }
        public class DoCreateDrawerScript : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var type = Path.GetFileNameWithoutExtension(pathName);
                StringBuilder script = new StringBuilder();
                script.Append("using System.Collections;\r\n");
                script.Append("using System.Collections.Generic;\r\n");
                script.Append("using UnityEditor;\r\n");
                script.Append("using UnityEngine;\r\n");
                script.Append($"[CustomPropertyDrawer(typeof({type.Replace("Drawer","")}), true)]\r\n");
                script.Append($"public class {type} : PropertyDrawer\r\n");
                script.Append("{\r\n");
                script.Append("    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)\r\n");
                script.Append("    {\r\n");
                script.Append("        \r\n");
                script.Append("    }\r\n");
                script.Append("\r\n");
                script.Append("    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)\r\n");
                script.Append("    {\r\n");
                script.Append("        return base.GetPropertyHeight(property, label);\r\n");
                script.Append("    }\r\n");
                script.Append("}\r\n");

                File.WriteAllText(pathName, script.ToString(), new UTF8Encoding(true, false));

                AssetDatabase.ImportAsset(pathName);
                MonoScript asset = AssetDatabase.LoadAssetAtPath<MonoScript>(pathName);
                ProjectWindowUtil.ShowCreatedAsset(asset);
            }

        }
    }
}

