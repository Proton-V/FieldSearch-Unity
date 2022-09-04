using FieldSearch.Core.Inspectors.Base;
using FieldSearch.Settings;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Core.Inspectors.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
    public class FieldSearchGlobalEditor : UnityEditor.Editor
    {
        FieldSearchSettings Settings => FieldSearchSettings.Instance;

        BaseSearchableEditorConfigObject SearchableEditor => Settings.SearchableEditor;

        bool IsActive => Settings?.ApplyToAll ?? false;

        private void OnEnable()
        {
            if (IsActive)
            {
                SearchableEditor.OnEnableInspector(target, serializedObject);
            }
        }

        private void OnDisable()
        {
            if (IsActive)
            {
                SearchableEditor.OnDisableInspector(target);
            }
        }

        public override void OnInspectorGUI()
        {
            if (IsActive)
            {
                SearchableEditor.OnInspectorGUI(serializedObject, base.OnInspectorGUI);
            }
            else
            {
                base.OnInspectorGUI();
            }
        }
    }
}
