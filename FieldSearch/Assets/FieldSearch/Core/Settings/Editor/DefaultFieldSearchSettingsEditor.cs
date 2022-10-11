using FieldSearch.Helpers.Cache;
using FieldSearch.Settings.Base;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Settings.Editor
{
    [CustomEditor(typeof(BaseFieldSearchSettings), true)]
    public class DefaultFieldSearchSettingsEditor : UnityEditor.Editor
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
