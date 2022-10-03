using FieldSearch.Core.Data.Criteria.Base;
using UnityEditor;
using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Core.Data.Criteria
{
	public class ByObjNameSearchCriterion : BaseSearchCriterion
	{
		public const SearchFilter CRITERION_SEARCH_FILTER = SearchFilter.ByObjName;

		public ByObjNameSearchCriterion() : base() { }

		protected override SearchFilter GetCriterionSearchFilter() => CRITERION_SEARCH_FILTER;

		public override bool HasResult<T>(SearchFilter currentFlags, params T[] input)
		{
			if (!IsActive(currentFlags))
			{
				return false;
			}

			if (input.Length < 2)
			{
				return false;
			}

			var rawSearchText = input[0] as string;
			if (rawSearchText is null)
			{
				return false;
			}
            
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

				return Compare(serializedProperty.objectReferenceValue.name, rawSearchText, currentFlags);
			}
            catch
            {
				// TODO: check SerializedProperty pptr error && update logic
			}

			return false;
		}
    }
}
