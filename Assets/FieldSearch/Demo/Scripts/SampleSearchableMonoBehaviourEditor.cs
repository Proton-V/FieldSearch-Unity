using FieldSearch.Editor;
using UnityEditor;

namespace FieldSearch.Samples.Editor
{
	[CustomEditor(typeof(SampleSearchableMonoBehaviour))]
	public class SampleSearchableMonoBehaviourEditor : SearchableEditor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			// Your code
			// ...
		}
	}
}
