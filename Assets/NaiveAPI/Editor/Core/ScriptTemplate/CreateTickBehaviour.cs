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
        public class CreateTickBehaviour : UnityEditor.Editor
        {
            [MenuItem("Assets/Create/Naive API/C# Tick Script")]
            static void CreateExampleAssets()
            {
                Texture2D icon = EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

                var endNameEditAction = CreateInstance<DoCreateTickBehaviour>();

                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, endNameEditAction, "NewTickBehaviour.cs", icon, "");
            }
        }
        public class DoCreateTickBehaviour : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var type = Path.GetFileNameWithoutExtension(pathName);
                StringBuilder script = new StringBuilder();
                script.Append("using System.Collections;\r\n");
                script.Append("using System.Collections.Generic;\r\n");
                script.Append("using UnityEngine;\r\n");
                script.Append("using NaiveAPI.GameTickSystem;\r\n");
                script.Append($"public class {type} : MonoBehaviour,ITickUpdate\r\n");
                script.Append("{\r\n");
                script.Append("    public TickUpdateInfo updateInfo { get; set; } = new TickUpdateInfo();\r\n");
                script.Append("    void Start()\r\n");
                script.Append("    {\r\n");
                script.Append("        ((ITickUpdate)this).Start(); // Setup your update frequence here\r\n");
                script.Append("    }\r\n");
                script.Append("\r\n");
                script.Append("    public void TickUpdate()\r\n");
                script.Append("    {\r\n");
                script.Append("        \r\n");
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
