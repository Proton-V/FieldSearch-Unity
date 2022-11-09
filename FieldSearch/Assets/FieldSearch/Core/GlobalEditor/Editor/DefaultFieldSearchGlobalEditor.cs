using FieldSearch.Settings.Base;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FieldSearch.Core.GlobalEditor
{
    /// <summary>
    /// Default implementation <see cref="BaseFieldSearchGlobalEditor"/>
    /// </summary>
    [CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
    public class DefaultFieldSearchGlobalEditor : BaseFieldSearchGlobalEditor
    {
        protected virtual BaseFieldSearchSettings Settings => BaseFieldSearchSettings.Instance;
        protected virtual Type SearchLayerInspectorType => Settings.SearchLayerInspectorType;
        protected virtual bool IsActive => Settings?.ApplyToAll ?? false;

        protected Editor searchLayerInspector;

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
        protected Editor searchableGlobalEditor;
        protected Editor defaultEditor;

        private void OnEnable()
        {
            //Try invoke baase OnEnable method
            typeof(T)
                .GetMethod(nameof(OnEnable),
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(this, null);

            InitSearchableInspector();
        }

        protected virtual void InitSearchableInspector()
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
