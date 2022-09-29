using FieldSearch.Settings;
using System;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Core.Inspectors.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
    public class FieldSearchGlobalEditor : UnityEditor.Editor
    {
        FieldSearchSettings Settings => FieldSearchSettings.Instance;
        Type SearchLayerInspectorType => Settings.SearchLayerInspectorType;

        UnityEditor.Editor searchLayerInspector;

        bool IsActive => Settings?.ApplyToAll ?? false;

        private void OnEnable()
        {
            if (IsActive)
            {
                searchLayerInspector = CreateEditor(target, SearchLayerInspectorType);
            }
        }

        private void OnDisable()
        {
            if (IsActive)
            {
                DestroyImmediate(searchLayerInspector);
            }
        }

        public override void OnInspectorGUI()
        {
            if (IsActive)
            {
                searchLayerInspector?.OnInspectorGUI();
            }
            else
            {
                base.OnInspectorGUI();
            }
        }
    }
}
