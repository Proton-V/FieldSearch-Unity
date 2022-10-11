using FieldSearch.Settings.Base;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Core.GlobalEditor
{
    [CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
    public class DefaultFieldSearchGlobalEditor : BaseFieldSearchGlobalEditor
    {
        BaseFieldSearchSettings Settings => BaseFieldSearchSettings.Instance;
        Type SearchLayerInspectorType => Settings.SearchLayerInspectorType;

        Editor searchLayerInspector;

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

    /// <summary>
    /// Abstract <see cref="DefaultFieldSearchGlobalEditor"/> to save default Inspector
    /// </summary>
    /// <typeparam name="T">Default inspector</typeparam>
    public abstract class DefaultFieldSearchGlobalEditor<T> : BaseFieldSearchGlobalEditor where T : Editor
    {
        Editor searchableGlobalEditor;
        Editor defaultEditor;

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
            searchableGlobalEditor = CreateEditor(target, typeof(DefaultFieldSearchGlobalEditor));
            defaultEditor = CreateEditor(target, typeof(T));
        }

        public override void OnInspectorGUI()
        {
            searchableGlobalEditor?.OnInspectorGUI();
            defaultEditor?.OnInspectorGUI();
        }
    }
}
