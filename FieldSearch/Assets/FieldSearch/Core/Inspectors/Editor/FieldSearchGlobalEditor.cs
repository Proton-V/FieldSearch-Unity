using FieldSearch.Settings;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Inspector = UnityEditor.Editor;

namespace FieldSearch.Core.Inspectors.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
    public class FieldSearchGlobalEditor : Inspector
    {
        FieldSearchSettings Settings => FieldSearchSettings.Instance;
        Type SearchLayerInspectorType => Settings.SearchLayerInspectorType;

        Inspector searchLayerInspector;

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

    public abstract class FieldSearchGlobalEditor<T> : Inspector where T : Inspector
    {
        Inspector searchableGlobalEditor;
        Inspector defaultEditor;

        private void OnEnable()
        {
            //Try invoke baase OnEnable method
            typeof(T)
                .GetMethod(nameof(OnEnable),
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(this, null);

            InitSearchableInspector();
        }

        private void InitSearchableInspector()
        {
            searchableGlobalEditor = CreateEditor(target, typeof(FieldSearchGlobalEditor));
            defaultEditor = CreateEditor(target, typeof(T));
        }

        public override void OnInspectorGUI()
        {
            searchableGlobalEditor?.OnInspectorGUI();
            defaultEditor?.OnInspectorGUI();
        }
    }
}
