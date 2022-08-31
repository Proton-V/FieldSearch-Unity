using FieldSearch.Data.Criteria.Base;
using FieldSearch.Helpers;
using UnityEditor;
using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Data.Criteria
{
	public class ByObjNameSearchCriterion : BaseSearchCriterion
	{
		public ByObjNameSearchCriterion(ref SearchFilter searchFilter)
			: base(ref searchFilter) { }

		bool StartWith => searchFilter.HasFlag(SearchFilter.StartWith);
		bool ByObjName => searchFilter.HasFlag(SearchFilter.ByObjName);

		public override bool HasResult<T>(params T[] input)
		{
			var rawSearchText = input[0] as string;
			if (rawSearchText is null)
			{
				return false;
			}
			var finalSearchText = SearchStringFormatter.GetFinalString(rawSearchText, searchFilter);
			var serializedProperty = input[1] as SerializedProperty;

			if(serializedProperty == null ||
				serializedProperty.objectReferenceValue == null)
            {
				return false;
            }

			if (ByObjName && serializedProperty.propertyType == SerializedPropertyType.ObjectReference)
			{
				return StartWith ?
				SearchStringFormatter.GetFinalString(serializedProperty.objectReferenceValue.name, searchFilter)
				.StartsWith(finalSearchText)
				: SearchStringFormatter.GetFinalString(serializedProperty.objectReferenceValue.name, searchFilter)
				.Contains(finalSearchText);
			}

			return false;
		}
	}
}
