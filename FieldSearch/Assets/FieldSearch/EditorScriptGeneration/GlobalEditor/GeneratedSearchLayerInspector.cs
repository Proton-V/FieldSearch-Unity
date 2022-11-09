using FieldSearch.Core.Inspectors;
using UnityEditor;

namespace FieldSearch.EditorScriptGeneration.GlobalEditor
{
	/// <summary>
	/// GeneratedSearchLayer implementation <see cref="Core.Inspectors.Base.BaseSearchLayerInspector"/>
	/// </summary>
	public class GeneratedSearchLayerInspector : DefaultSearchLayerInspector
	{
		public override void OnInspectorGUI()
		{
			searchInspectorService.ShowSearchTextArea();

			if (!searchInspectorService.ShowSearchObjectsLayer() || searchInspectorService.IsNullOrNone)
			{
				if (!searchInspectorService.IsNullOrNone)
				{
					EditorGUILayout.HelpBox("No results found!", MessageType.Info);
				}
			}

			serializedObject.Update();
			serializedObject.ApplyModifiedProperties();
		}
	}
}
