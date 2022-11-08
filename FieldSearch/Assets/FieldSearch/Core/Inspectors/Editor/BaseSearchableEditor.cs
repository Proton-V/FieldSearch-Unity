using FieldSearch.Core.Inspectors.Base;

namespace FieldSearch.Core.Inspectors.Editor.Base
{
    /// <summary>
    /// Base class for manually created SearchableEditor's
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseSearchableEditor<T> : UnityEditor.Editor
    where T : BaseSearchLayerInspector
    {
        protected UnityEditor.Editor searchLayerInspector;

        protected void OnEnable()
        {
            searchLayerInspector = CreateEditor(target, typeof(T));
        }

        private void OnDisable()
        {
            DestroyImmediate(searchLayerInspector);
        }

        public override void OnInspectorGUI()
        {
            searchLayerInspector?.OnInspectorGUI();
        }
    }
}
