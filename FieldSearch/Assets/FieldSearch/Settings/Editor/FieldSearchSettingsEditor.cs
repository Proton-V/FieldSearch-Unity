using FieldSearch.EditorScriptGeneration.Editor;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Settings.Editor
{
    [CustomEditor(typeof(FieldSearchSettings), true)]
    public class FieldSearchSettingsEditor : DefaultFieldSearchSettingsEditor
    {
        FieldSearchSettings fieldSearchSettings;

        private void OnEnable()
        {
            fieldSearchSettings = target as FieldSearchSettings;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(20);
            if (GUILayout.Button("Open EditorScriptGenerator Window"))
            {
                EditorScriptGeneratorWindow.Init(fieldSearchSettings.EditorScriptGeneratorSettings);
            }
        }
    }
}
