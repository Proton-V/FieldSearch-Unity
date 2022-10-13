using FieldSearch.Core.GlobalEditor;
using System;
using Inspector = UnityEditor.Editor;

namespace FieldSearch.EditorScriptGeneration.GlobalEditor
{
    public class GeneratedFieldSearchGlobalEditor : DefaultFieldSearchGlobalEditor
    {
        protected override Type SearchLayerInspectorType => typeof(GeneratedSearchLayerInspector);
        protected override bool IsActive => true;

        public override void OnInspectorGUI()
        {
            searchLayerInspector?.OnInspectorGUI();
        }
    }

    /// <summary>
    /// Abstract <see cref="GeneratedFieldSearchGlobalEditor"/> to save default Inspector
    /// </summary>
    /// <typeparam name="T">Default inspector</typeparam>
    public abstract class GeneratedFieldSearchGlobalEditor<T> : DefaultFieldSearchGlobalEditor<T> where T : Inspector
    {
        protected override void InitSearchableInspector()
        {
            searchableGlobalEditor = CreateEditor(target, typeof(GeneratedFieldSearchGlobalEditor));
            defaultEditor = CreateEditor(target, typeof(T));
        }
    }
}
