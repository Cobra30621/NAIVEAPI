using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DocumentBuilder;
namespace NaiveAPI
{
    public class NaiveApiDocsMenuItem
    {
        // menuitem path
        [MenuItem("Tools/NaiveAPI/NaiveAPI docs")]
        public static void OpenNaiveApiDocs()
        {
            // get SODocInformation object
            SODocInformation root = AssetDatabase.LoadAssetAtPath<SODocInformation>
                        (NaiveApiData.Path.NaiveApiRoot + "/Editor/Docs/NaiveAPI_Docs.asset");

            // open docs window with specific book root
            DocumentBuilderWindow.ShowWindow(root);
        }
    }
}
