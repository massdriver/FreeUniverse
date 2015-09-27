
using FreeUniverse.Common.Arch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

            if (GUILayout.Button("Export"))
            {
                ArchSystem obj = ((SystemRoot)target).ToArchObject() as ArchSystem;
                File.WriteAllText(EditorUtility.SaveFilePanelInProject("Save as txt arch", obj.nickname, "txt", null), JsonConvert.SerializeObject(obj, Formatting.Indented));
            }
        }
    }
}
