using System;
using FieldSearch.Core.Inspectors.Base;

namespace FieldSearch.Core.Inspectors.Editor.Base
{
    public abstract class BaseSearchableEditor<T> : UnityEditor.Editor
    where T : BaseSearchableEditorConfigObject
    {
        protected T searchableEditorObject;

        protected void OnEnable()
        {
            if (searchableEditorObject == null)
            {
                searchableEditorObject = (T)Activator.CreateInstance(typeof(T));
            }

            searchableEditorObject.OnEnableInspector(target, serializedObject);
        }

        protected void OnDisable()
        {
            searchableEditorObject.OnDisableInspector(target);
        }

        public override void OnInspectorGUI()
        {
            searchableEditorObject.OnInspectorGUI(serializedObject, base.OnInspectorGUI);
        }
    }
}
