using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Core.Inspectors.Controllers
{
	/// <summary>
	/// Default Search Service for Inspectors.
	/// Using <see cref="SearchWithFilters"/>
	/// </summary>
	public class SearchInspectorService
	{
		public SearchInspectorService(SerializedObject serializedObject)
		{
			searchFilters =
				SearchFilter.IgnoreCase |
				SearchFilter.ByFieldName |
				SearchFilter.ByObjName;

			search = new SearchWithFilters(searchFilters);
			this.serializedObject = serializedObject;
		}

		public SerializedObject SerializedObject
		{
			get => serializedObject;
			set => serializedObject = value;
		}

		/// <summary>
		/// Search status is inactive
		/// </summary>
		public bool IsNullOrNone => string.IsNullOrEmpty(searchText) || searchFilters.Equals(SearchFilter.None);

		/// <summary>
		/// Current target object
		/// </summary>
		private object TargetObject => serializedObject?.targetObject;

		private SearchWithFilters search;
		private SerializedObject serializedObject;
		readonly string searchLabel = "Field Search:";

		private string searchText;
		private SearchFilter searchFilters;

		/// <summary>
		/// Get current search data
		/// with <see cref="searchText"/> & <see cref="searchFilters"/>
		/// </summary>
		/// <returns></returns>
		public (string searchText, int flags) GetData()
		{
			return (searchText, (int)searchFilters);
		}

		/// <summary>
		/// Update current search data
		/// </summary>
		/// <param name="searchText"></param>
		/// <param name="searchFilters"></param>
		public void UpdateData(string searchText, SearchFilter searchFilters)
		{
			if (!string.IsNullOrEmpty(searchText))
			{
				this.searchText = searchText;
			}

			this.searchFilters = searchFilters;
			search.UpdateCriteria(this.searchFilters);

			serializedObject?.ApplyModifiedProperties();
		}

		/// <summary>
		/// Try show full inspector search layer
		/// </summary>
		/// <returns></returns>
		public bool ShowInspectorLayer()
        {
            if (!ShowSearchTextArea())
            {
				return false;
            }

			return ShowSearchObjectsLayer();
		}

		/// <summary>
		/// Try show only search layer
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Try show only search result layer
		/// </summary>
		/// <returns></returns>
		public bool ShowSearchObjectsLayer()
		{
			if (IsNullOrNone)
			{
				return false;
			}

			var rawProperties = GetFieldInfoRecursive(TargetObject.GetType())
				.Where(x => x.GetCustomAttributes(typeof(SerializeField), false) != null)
				.Select(x => serializedObject.FindProperty(x.Name))
				.Where(x => x != null);

			var properties = new List<SerializedProperty>();
			foreach (var prop in rawProperties)
            {
                if (prop.isArray)
                {
					var childProps = GetSerializedPropertyRecursive(prop,
						x => search.GetResult(searchFilters, true, searchText, x));

					properties.AddRange(childProps);
					continue;
                }

                if (!prop.isArray && search.GetResult(searchFilters, true, searchText, prop))
                {
					properties.Add(prop);
					continue;
				}
            }

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

		/// <summary>
		/// Get all <see cref="SerializedProperty"/> based on <paramref name="property"/>
		/// with inherited objs
		/// </summary>
		/// <param name="property"></param>
		/// <param name="validateFunc"></param>
		/// <returns></returns>
		private List<SerializedProperty> GetSerializedPropertyRecursive(SerializedProperty property, 
			Func<SerializedProperty, bool> validateFunc)
        {
			var result = new List<SerializedProperty>();

			if (validateFunc(property))
            {
				result.Add(property);
				return result;
            }

			for (int i = 0; i < property.arraySize; i++)
			{
				var prop = property.GetArrayElementAtIndex(i);

				if (prop.isArray)
				{
					var arrayProps = GetSerializedPropertyRecursive(prop, validateFunc);
                    if (arrayProps.Count != 0)
                    {
                        result.Add(prop);
                    }
                    continue;
                }
                else
                {
                    if (validateFunc(prop))
                    {
						result.Add(prop);
					}
					continue;
				}
			}

			return result;
		}

		/// <summary>
		/// Get all <see cref="FieldInfo"/> for <paramref name="type"/>
		/// with inherited objs
		/// </summary>
		/// <param name="property"></param>
		/// <param name="validateFunc"></param>
		/// <returns></returns>
		private List<FieldInfo> GetFieldInfoRecursive(Type type)
        {
			var result = 
			type
				.GetFields(
				BindingFlags.NonPublic
				| BindingFlags.Public
				| BindingFlags.Instance)

				.ToList();

			if(type.BaseType != null)
            {
				result.AddRange(GetFieldInfoRecursive(type.BaseType));
			}

			return result;
		}

		/// <summary>
		/// Just show search layer
		/// </summary>
		/// <returns></returns>
		private bool ShowSearchFields()
		{
			var searchTextChanged =
				ActionWithChangeCheck(() => searchText = EditorGUILayout.TextField(searchLabel, searchText));
			var searchFiltersChanged =
				ActionWithChangeCheck(() => searchFilters = (SearchFilter)EditorGUILayout.EnumFlagsField(searchFilters));

			if (searchFiltersChanged)
			{
				serializedObject?.ApplyModifiedProperties();
				search.UpdateCriteria(searchFilters);
			}

			if (searchTextChanged || string.IsNullOrEmpty(searchText))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Method to call GUI action that returns state of change
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Default call <see cref="EditorGUILayout.EndVertical()"/> with extra actions
		/// </summary>
		/// <returns></returns>
		private bool EndVertical()
		{
			serializedObject?.ApplyModifiedProperties();
			EditorGUILayout.EndVertical();
			return false;
		}
	}
}
