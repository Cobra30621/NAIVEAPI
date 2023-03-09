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
        public class CreateEditorScript : UnityEditor.Editor
        {
            [MenuItem("Assets/Create/Naive API/ScriptTemplate/Editor Script")]
            static void CreateExampleAssets()
            {
                Texture2D icon = EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

                var endNameEditAction = CreateInstance<DoCreateEditorScript>();

                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, endNameEditAction, "NewEditor.cs", icon, "");
            }
        }
        public class DoCreateEditorScript : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var type = Path.GetFileNameWithoutExtension(pathName);
                StringBuilder script = new StringBuilder();
                script.Append("using System.Collections;\r\n");
                script.Append("using System.Collections.Generic;\r\n");
                script.Append("using UnityEditor;\r\n");
                script.Append("using UnityEngine;\r\n");
                script.Append($"[CustomEditor(typeof({type.Replace("Editor","")}))]\r\n");
                script.Append($"public class {type} : Editor\r\n");
                script.Append("{\r\n");
                script.Append("    object m_target;\r\n");
                script.Append("    private void OnEnable()\r\n");
                script.Append("    {\r\n");
                script.Append($"        m_target = target as {type.Replace("Editor","")};\r\n");
                script.Append("    }\r\n");
                script.Append("    public override void OnInspectorGUI()\r\n");
                script.Append("    {\r\n");
                script.Append("\r\n");
                script.Append("        EditorUtility.SetDirty(target); // if you need to save serialize object\r\n");
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
