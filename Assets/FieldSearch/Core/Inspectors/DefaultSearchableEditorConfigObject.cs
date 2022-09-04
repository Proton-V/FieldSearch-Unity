using FieldSearch.Core.Inspectors.Base;
using FieldSearch.Helpers.Cache;
using FieldSearch.Helpers.Cache.Data;
using System;
using UnityEditor;
using UnityEngine;
using static FieldSearch.Core.Base.BaseSearch;
using Object = UnityEngine.Object;

namespace FieldSearch.Core.Inspectors
{
	[CreateAssetMenu(fileName = "DefaultSearchableEditorConfigObject", menuName = "ScriptableObjects/FieldSearch/Searchable Editor/Default Searchable Editor Config Object")]
	public class DefaultSearchableEditorConfigObject : BaseSearchableEditorConfigObject
	{
		protected Func<Object, int> Id => (target) => target.GetInstanceID();
		private SearchInspector searchInspector;

		public override void OnEnableInspector(Object target, SerializedObject serializedObject)
		{
			var id = Id(target);
			var cachedData = SearchInspectorCache.TryGetValue(id);

			searchInspector = new SearchInspector(serializedObject);
			searchInspector.UpdateData(cachedData.searchText, (SearchFilter)cachedData.flags);
		}

		public override void OnDisableInspector(Object target)
		{
			var id = Id(target);
			var data = searchInspector.GetData();
			SearchInspectorCache.AddValue(new SearchCacheObj(id, data.searchText, data.flags));
		}

		public override void OnInspectorGUI(SerializedObject serializedObject, Action defaultOnInspectorGUI)
		{
			searchInspector.ShowSearchTextArea();

			if (!searchInspector.ShowSearchObjectsLayer() || searchInspector.IsNullOrNone)
			{
				if (!searchInspector.IsNullOrNone)
				{
					EditorGUILayout.HelpBox("No results found!", MessageType.Info);
				}

				GUILayout.Space(3);
				defaultOnInspectorGUI();
			}

			serializedObject.Update();
			serializedObject.ApplyModifiedProperties();
		}
	}
}
