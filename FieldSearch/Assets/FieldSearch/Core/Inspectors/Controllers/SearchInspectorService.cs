using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Core.Inspectors.Controllers
{
	public class SearchInspectorService
	{
		public SearchInspectorService(SerializedObject serializedObject)
		{
			searchFilters =
				SearchFilter.IgnoreCase |
				SearchFilter.ByFieldName |
				SearchFilter.ByObjName;

			search = new SearchWithFilters(ref searchFilters);
			this.serializedObject = serializedObject;
		}

		public SerializedObject SerializedObject
		{
			get => serializedObject;
			set => serializedObject = value;
		}

		public bool IsNullOrNone => string.IsNullOrEmpty(searchText) || searchFilters.Equals(SearchFilter.None);
		private object TargetObject => serializedObject?.targetObject;

		private SearchWithFilters search;
		private SerializedObject serializedObject;
		readonly string searchLabel = "Field Search:";

		private string searchText;
		private SearchFilter searchFilters;

		public (string searchText, int flags) GetData()
		{
			return (searchText, (int)searchFilters);
		}

		public void UpdateData(string searchText, SearchFilter searchFilters)
		{
			if (!string.IsNullOrEmpty(searchText))
			{
				this.searchText = searchText;
			}

			this.searchFilters = searchFilters;
			search.UpdateCriteria(ref this.searchFilters);

			serializedObject?.ApplyModifiedProperties();
		}

		public bool ShowInspectorLayer()
        {
            if (!ShowSearchTextArea())
            {
				return false;
            }

			return ShowSearchObjectsLayer();
		}

		public bool ShowSearchTextArea()
		{
			try
			{
				if (SerializedObject == null)
				{
					return false;
				}

				EditorGUILayout.BeginVertical(EditorStyles.helpBox);

				if (!ShowSearchFields())
				{
					EndVertical();
					return false;
				}

				EndVertical();
				return true;
			}
			catch (Exception e)
			{
				Debug.LogError($"Fail with SearchArea: {e}");
				return false;
			}
		}

		public bool ShowSearchObjectsLayer()
		{
            if (IsNullOrNone)
            {
				return false;
            }

			var properties = TargetObject.GetType()
				.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
				.Where(x => x.GetCustomAttributes(typeof(SerializeField), false) != null)
				.Select(x => serializedObject.FindProperty(x.Name))

				.Where(x => search.GetResult(true, searchText, x));

			if (properties == null || !properties.Any())
			{
				return false;
			}

			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			EditorGUILayout.Space();

			foreach (var prop in properties)
			{
				EditorGUILayout.PropertyField(prop, true);
			}

			EndVertical();
			return true;
		}

		private bool ShowSearchFields()
		{
			var searchTextChanged =
				ActionWithChangeCheck(() => searchText = EditorGUILayout.TextField(searchLabel, searchText));
			var searchFiltersChanged =
				ActionWithChangeCheck(() => searchFilters = (SearchFilter)EditorGUILayout.EnumFlagsField(searchFilters));

			if (searchFiltersChanged)
			{
				serializedObject?.ApplyModifiedProperties();
				search.UpdateCriteria(ref searchFilters);
			}

			if (searchTextChanged || string.IsNullOrEmpty(searchText))
			{
				return false;
			}

			return true;
		}

		private bool ActionWithChangeCheck(Action action)
		{
			EditorGUI.BeginChangeCheck();
			action.Invoke();
			if (EditorGUI.EndChangeCheck())
			{
				return true;
			}
			return false;
		}

		private bool EndVertical()
		{
			serializedObject?.ApplyModifiedProperties();
			EditorGUILayout.EndVertical();
			return false;
		}
	}
}
