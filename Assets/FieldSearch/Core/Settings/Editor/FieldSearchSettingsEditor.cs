using FieldSearch.Helpers.Cache;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Settings.Editor
{
    [CustomEditor(typeof(FieldSearchSettings))]
    public class FieldSearchSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(20);
            if(GUILayout.Button("Clear cache from disk"))
            {
                SearchInspectorCache.ClearCache(true, false);
            }
            if (GUILayout.Button("Clear cache from memory"))
            {
                SearchInspectorCache.ClearCache(false, true);
            }
            if (GUILayout.Button("Clear all cache"))
            {
                SearchInspectorCache.ClearCache();
            }
        }
    }
}
