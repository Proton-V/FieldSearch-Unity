using FieldSearch.Core.Inspectors.Base;
using FieldSearch.Core.Inspectors.Controllers;
using FieldSearch.Helpers.Cache;
using FieldSearch.Helpers.Cache.Data;
using System;
using UnityEditor;
using UnityEngine;
using static FieldSearch.Core.Base.BaseSearch;
using Object = UnityEngine.Object;

namespace FieldSearch.Core.Inspectors
{
	public class DefaultSearchLayerInspector : BaseSearchLayerInspector
	{
		protected Func<Object, int> Id => (target) => target.GetInstanceID();
		private SearchInspectorService searchInspectorService;

		protected virtual void OnEnable()
		{
			var id = Id(target);
			searchInspectorService = new SearchInspectorService(serializedObject);

			SearchCacheObj cachedData;
			if (SearchInspectorCache.TryGetValue(id, out cachedData))
            {
				searchInspectorService.UpdateData(cachedData.searchText, (SearchFilter)cachedData.flags);
			}
		}

		protected virtual void OnDisable()
		{
			var id = Id(target);
			var data = searchInspectorService.GetData();
			SearchInspectorCache.TryAddValue(new SearchCacheObj(id, data.searchText, data.flags));
		}

		public override void OnInspectorGUI()
		{
			searchInspectorService.ShowSearchTextArea();

			if (!searchInspectorService.ShowSearchObjectsLayer() || searchInspectorService.IsNullOrNone)
			{
				if (!searchInspectorService.IsNullOrNone)
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
