using FieldSearch.Core.Data.Criteria.Base;
using UnityEditor;
using static FieldSearch.Core.Base.BaseSearch;

namespace FieldSearch.Core.Data.Criteria
{
	public class ByFieldNameSearchCriterion : BaseSearchCriterion
	{
		public const SearchFilter CRITERION_SEARCH_FILTER = SearchFilter.ByFieldName;

		public ByFieldNameSearchCriterion() : base() { }

		protected override SearchFilter GetCriterionSearchFilter() => CRITERION_SEARCH_FILTER;

		public override bool HasResult<T>(SearchFilter currentFlags, params T[] input)
		{
			if (!IsActive(currentFlags))
			{
				return false;
			}

			var rawSearchText = input[0] as string;
			if(rawSearchText is null)
            {
				return false;
            }

			var serializedProperty = input[1] as SerializedProperty;

			if (serializedProperty == null)
			{
				return false;
			}

			return Compare(serializedProperty.displayName, rawSearchText, currentFlags);
		}
	}
}
