using FieldSearch.Core.Data.Criteria.Base;
using FieldSearch.Helpers.StringFormatter;
using UnityEditor;
using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Core.Data.Criteria
{
	public class ByObjNameSearchCriterion : BaseSearchCriterion
	{
		public ByObjNameSearchCriterion(ref SearchFilter searchFilter)
			: base(ref searchFilter) { }

		bool StartWith => searchFilter.HasFlag(SearchFilter.StartWith);
		bool ByObjName => searchFilter.HasFlag(SearchFilter.ByObjName);

		public override bool HasResult<T>(params T[] input)
		{
			if (input.Length < 2)
			{
				return false;
			}

			var rawSearchText = input[0] as string;
			if (rawSearchText is null)
			{
				return false;
			}

			var finalSearchText = SearchStringFormatter.GetFinalString(rawSearchText, searchFilter);
            
			try
            {
				if (input[1] is SerializedProperty == false)
				{
					return false;
				}

				var serializedProperty = input[1] as SerializedProperty;

				if(serializedProperty == null
					|| serializedProperty.propertyType != SerializedPropertyType.ObjectReference)
                {
					return false;
                }

				if (ByObjName)
				{
					var finalString = SearchStringFormatter.GetFinalString(serializedProperty.objectReferenceValue.name, searchFilter);
					return StartWith ?
					finalString.StartsWith(finalSearchText)
					: finalString.Contains(finalSearchText);
				}
			}
            catch
            {
				// TODO: check SerializedProperty pptr error && update logic
			}

			return false;
		}
	}
}
