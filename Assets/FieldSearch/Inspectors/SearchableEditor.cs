using FieldSearch.Core.Inspectors;
using FieldSearch.Helpers;
using UnityEditor;
using UnityEngine;
using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Editor
{
	[CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
	public class SearchableEditor : UnityEditor.Editor
	{
		protected int Id => target.GetHashCode();
		protected SearchInspector searchInspector;

		public virtual void OnEnable()
		{
			if (searchInspector == null)
			{
				var cachedData = SearchInspectorCache.TryGetValue(Id);

				searchInspector = new SearchInspector(serializedObject);
				searchInspector.UpdateData(cachedData.searchText, (SearchFilter)cachedData.flags);
			}
		}

		public virtual void OnDisable()
		{
			var data = searchInspector.GetData();
			SearchInspectorCache.AddValue(new Helpers.Data.SearchCacheObj(Id, data.searchText, data.flags));
		}

		public override void OnInspectorGUI()
		{
			searchInspector.ShowSearchTextArea();

            if (!searchInspector.ShowSearchObjectsLayer() || searchInspector.IsNullOrNone)
            {
                if (!searchInspector.IsNullOrNone)
                {
                    EditorGUILayout.HelpBox("No results found!", MessageType.Info);
                }

                GUILayout.Space(3);
                base.OnInspectorGUI();
            }

            serializedObject.Update();
			serializedObject.ApplyModifiedProperties();
		}
	}
}
