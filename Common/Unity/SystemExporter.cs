
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    [CustomEditor(typeof(SystemRoot))]
    public sealed class SystemExporter : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Export To File"))
            {
                ExportSystemToFile(target as SystemRoot);
            }
        }

        private void ExportSystemToFile(SystemRoot root)
        {
            if (root == null)
            {
                Debug.Log("System root is null");
                return;
            }

            //string path = EditorUtility.OpenFilePanel( "Save System", "", "system");
        }
    }
}
