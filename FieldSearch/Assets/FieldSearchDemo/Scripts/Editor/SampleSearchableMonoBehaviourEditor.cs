using FieldSearch.Core.Inspectors;
using FieldSearch.Core.Inspectors.Editor.Base;
using UnityEditor;

namespace FieldSearch.Samples.Editor
{
    [CustomEditor(typeof(SampleSearchableMonoBehaviour))]
    public class SampleSearchableMonoBehaviourEditor : BaseSearchableEditor<DefaultSearchLayerInspector>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Your code
            // ...
        }
    }
}
